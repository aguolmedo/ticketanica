using ticketanicav2.Models;

namespace ticketanicav2.Logic;

public class EventosService
{
    public List<Evento> Eventos;

    public Evento CrearEvento(string nombre,string artista, Direccion direccion, int capacidadMaxima)
    {
        var newEvento = new Evento(nombre,artista, direccion, capacidadMaxima);
        Eventos.Add(newEvento);
        return newEvento;
    }

    public EventosService()
    {
        Eventos = new List<Evento>();
    }
}