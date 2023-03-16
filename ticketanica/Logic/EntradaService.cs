using System.Text;
using ticketanica.DataLayer;
using ticketanicav2.Helpers;
using ticketanicav2.Logic.Interfaces;

namespace ticketanicav2.Logic;

public class EntradaService : IEntradaService
{
    private readonly IEventoService _eventoService;

    private readonly TicketanicaDbContext _ticketanicaDb;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public EntradaService(IEventoService eventoService, TicketanicaDbContext ticketanicaDb, IHttpContextAccessor httpContextAccessor)
    {
        _ticketanicaDb = ticketanicaDb;
        _eventoService = eventoService;
        _httpContextAccessor = httpContextAccessor;
    }

    public bool GenerarEntrada(int idEvento)
    {
        try
        {
            var evento = _eventoService.GetById(idEvento);

            if (evento.Entradas.Count >= evento.CapacidadMaxima) return false;
            var entrada = new Entrada()
            {
                Usada = false,
                EventoId = evento.IdEvento,
            };

            _ticketanicaDb.Entradas.Add(entrada);
            _ticketanicaDb.SaveChanges();
            entrada.CodigoQr = GenerarQr(entrada);
            _ticketanicaDb.Entradas.Update(entrada);
            _ticketanicaDb.SaveChanges();

            return true;
        }
        catch (ArgumentException e)
        {
            throw e;
        }
    }

    public bool ValidarEntrada(int idEvento, string codigoQr)
    {
        try
        {
            if (!_httpContextAccessor.HttpContext.Session.TryGetValue("usuario", out var usuarioSession))
                throw new ArgumentException("No existe ninguna sesión Activa");
            
            var userSession = Encoding.UTF8.GetString(usuarioSession);

            var evento = _eventoService.GetById(idEvento);

            if (evento.Organizador.Email != userSession)
                throw new ArgumentException("Solo el organizador puede validar una entrada! ");
            
            if (!evento.Entradas.ContainsKey(codigoQr)) return false;
       
            var entrada = evento.Entradas[codigoQr];

            var entradaDb = _ticketanicaDb.Entradas.Find(entrada.IdEntrada);
            
            if (entradaDb.Usada) throw new ArgumentException("Acceso denegado - La entrada ya fue usada!");
            entradaDb.Usada = true;

            _ticketanicaDb.Entradas.Update(entradaDb);
            _ticketanicaDb.SaveChanges();
        
            return true;
        }
        catch (ArgumentException e)
        {
            throw e;
        }
        

    }

    private static string GenerarQr(Entrada entrada)
    {
        var qrString = $"{entrada.IdEntrada}-{entrada.CreatedAt}-{entrada.EventoId}";
        var sha256 = EncryptHelper.GetSha256(qrString);

        return sha256;
    }
}