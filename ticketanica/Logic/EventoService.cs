
using Microsoft.EntityFrameworkCore;
using ticketanicav2.DataLayer;
using ticketanicav2.Logic.Interfaces;
using ticketanicav2.Models;
using Evento = ticketanicav2.Models.Evento;
using User = ticketanicav2.DataLayer.User;

namespace ticketanicav2.Logic;

public  class EventoService :IEventoService
{
    private readonly TicketanicaDbContext _ticketanicaDb;
    
    public EventoService(TicketanicaDbContext ticketanicaDb)
    {
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
            var direccionModel = new Direccion(evento.IdDireccionNavigation.CiudadName, evento.IdDireccionNavigation.CalleName, Convert.ToInt32(evento.IdDireccionNavigation.CalleNro), evento.IdDireccionNavigation.LocalName);
            var eventoModel = new Evento(evento.IdEvento,evento.EventoName, evento.ArtistaName, direccionModel, Convert.ToInt32(evento.CapacidadMaxima),organizadorModel);
            
            listaEventos.Add(eventoModel);
        }

        return listaEventos;
    }

    public int AddEvento(Evento evento)
    {
        var userDb = _ticketanicaDb.Users.Find(evento.Organizador.Email) ?? throw new ArgumentException("El usuario no existe.");
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

        var eventoDbModel = new DataLayer.Evento
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

    

    


}