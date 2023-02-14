using ticketanica.DataLayer;
using ticketanicav2.Helpers;
using ticketanicav2.Logic.Interfaces;

namespace ticketanicav2.Logic;

public class EntradaService : IEntradaService
{
    private readonly IEventoService _eventoService;

    private readonly TicketanicaDbContext _ticketanicaDb;

    public EntradaService(IEventoService eventoService, TicketanicaDbContext ticketanicaDb)
    {
        _ticketanicaDb = ticketanicaDb;
        _eventoService = eventoService;
    }

    public bool GenerarEntrada(int idEvento)
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

        entrada.CodigoQr = GenerarQR(entrada);
        _ticketanicaDb.Entradas.Update(entrada);
        _ticketanicaDb.SaveChanges();

        return true;
    }

    public bool ValidarEntrada(string codigoQr)
    {
        throw new NotImplementedException();
    }
    
    private string GenerarQR(Entrada entrada)
    {
        var qrString = $"{entrada.IdEntrada}-{entrada.CreatedAt}-{entrada.EventoId}";
        var sha256 = EncryptHelper.GetSha256(qrString);

        return sha256;
    }
}