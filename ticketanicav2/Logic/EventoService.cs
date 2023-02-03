
using Microsoft.EntityFrameworkCore;
using ticketanicav2.DataLayer;
using ticketanicav2.Logic.Interfaces;
using ticketanicav2.Models;
using Evento = ticketanicav2.Models.Evento;

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
            .Include(d => d.IdDireccionNavigation);

        var listaEventos = new List<Evento>();

        foreach (var evento in eventosDb)
        {
            var direccionModel = new Direccion(evento.IdDireccionNavigation.CiudadName, evento.IdDireccionNavigation.CalleName, Convert.ToInt32(evento.IdDireccionNavigation.CalleNro), evento.IdDireccionNavigation.LocalName);
            var eventoModel = new Evento(evento.IdEvento,evento.EventoName, evento.ArtistaName, direccionModel, Convert.ToInt32(evento.CapacidadMaxima));
            
            listaEventos.Add(eventoModel);
        }

        return listaEventos;
    }

    public bool AddEvento(Evento evento)
    {
        throw new NotImplementedException();
    }

    public bool ModifyEvento(int idEvento)
    {
        throw new NotImplementedException();
    }

    

    


}