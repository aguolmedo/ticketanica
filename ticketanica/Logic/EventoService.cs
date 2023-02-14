using System.Text;
using Microsoft.EntityFrameworkCore;
using ticketanica.DataLayer;
using ticketanicav2.Logic.Interfaces;
using ticketanicav2.Models;
using Entrada = ticketanicav2.Models.Entrada;
using Evento = ticketanicav2.Models.Evento;

namespace ticketanicav2.Logic;

public class EventoService : IEventoService
{
    private readonly TicketanicaDbContext _ticketanicaDb;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public EventoService(TicketanicaDbContext ticketanicaDb, IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _ticketanicaDb = ticketanicaDb;
    }

    public List<Evento> GetAll()
    {
        var eventosDb = _ticketanicaDb.Eventos
            .Include(d => d.IdDireccionNavigation)
            .Include(u => u.EmailOrganizadorNavigation);

        var listaEventos = new List<Evento>();

        foreach (var evento in eventosDb)
        {
            var organizadorModel = new Organizador(evento.EmailOrganizadorNavigation.Email);
            var direccionModel = new Direccion(evento.IdDireccionNavigation.CiudadName,
                evento.IdDireccionNavigation.CalleName, Convert.ToInt32(evento.IdDireccionNavigation.CalleNro),
                evento.IdDireccionNavigation.LocalName);
            var eventoModel = new Evento(evento.IdEvento, evento.EventoName, evento.ArtistaName, direccionModel,
                Convert.ToInt32(evento.CapacidadMaxima), organizadorModel);

            listaEventos.Add(eventoModel);
        }

        return listaEventos;
    }

    public Evento GetById(int id)
    {
        var eventoDb = _ticketanicaDb.Eventos
            .Include(d => d.IdDireccionNavigation)
            .Include(e => e.Entrada)
            .FirstOrDefault(e => e.IdEvento == id);
        
        if (eventoDb is null)
            throw new ArgumentException("No existe evento con ese Id");

        var entradas = new HashSet<Entrada>();
        foreach (var entrada in eventoDb.Entrada)
        {
            var entradaModel = new Entrada()
            {
                IdEntrada = entrada.IdEntrada,
                CodigoQr = entrada.CodigoQr,
                Usada = entrada.Usada,
                FechaCreacion = entrada.CreatedAt
            };
            entradas.Add(entradaModel);
        }

        var direccionModel = new Direccion()
        {
            NombreCiudad = eventoDb.IdDireccionNavigation.CiudadName,
            NombreCalle = eventoDb.IdDireccionNavigation.CalleName,
            NombreLocal = eventoDb.IdDireccionNavigation.LocalName,
            NumeroCalle = eventoDb.IdDireccionNavigation.CalleNro,
        };


        return new Evento()
        {
            IdEvento = eventoDb.IdEvento,
            Nombre = eventoDb.EventoName,
            Artista = eventoDb.ArtistaName,
            Entradas = entradas,
            Direccion = direccionModel,
            CapacidadMaxima = eventoDb.CapacidadMaxima,
            Organizador = new Organizador()
            {
                Email = eventoDb.EmailOrganizador
            }
        };
    }

    public int AddEvento(Evento evento)
    {
        if (!_httpContextAccessor.HttpContext.Session.TryGetValue("usuario", out var value))
            throw new ArgumentException("No existe ninguna sesión Activa");
        
        var userSession = Encoding.UTF8.GetString(value);
        var userDb = _ticketanicaDb.Users.Find(userSession) ?? throw new ArgumentException("El usuario no existe.");
        

        var direccionDb = _ticketanicaDb.Direcciones.FirstOrDefault(d => 
                              d.CalleName == evento.Direccion.NombreCalle &&
                              d.CalleNro == evento.Direccion.NumeroCalle &&
                              d.CiudadName == evento.Direccion.NombreCiudad &&
                              d.LocalName == evento.Direccion.NombreLocal) 
                          ?? new Direccione
                          {
                              CalleName = evento.Direccion.NombreCalle,
                              CalleNro = evento.Direccion.NumeroCalle,
                              CiudadName = evento.Direccion.NombreCiudad,
                              LocalName = evento.Direccion.NombreLocal
                          };

        var eventoDbModel = new ticketanica.DataLayer.Evento
        {
            EventoName = evento.Nombre,
            ArtistaName = evento.Artista,
            CapacidadMaxima = evento.CapacidadMaxima,
            IdDireccionNavigation = direccionDb,
            EmailOrganizadorNavigation = userDb
        };

        _ticketanicaDb.Eventos.Add(eventoDbModel);
        _ticketanicaDb.SaveChanges();
        return eventoDbModel.IdEvento;
    }

    public bool ModifyEvento(int idEvento)
    {
        throw new NotImplementedException();
    }

    public int generarEntrada(int idEvento)
    {
        return 9;
    }
}