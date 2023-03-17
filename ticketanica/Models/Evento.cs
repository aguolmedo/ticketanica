using System.ComponentModel.DataAnnotations;
using ticketanicav2.Helpers.CustomValidations;

namespace ticketanicav2.Models;

public class Evento
{
    public int? IdEvento { get; init; }
    
    public string Nombre { get; init; }

    public string Artista { get; init; }
    public Dictionary<string,Entrada>? Entradas { get; init; }

    public Direccion Direccion { get; init; }

    [Range(0,5000)]
    public int CapacidadMaxima { get; init; }

    public Organizador? Organizador { get; set; }
    
    [ValidarImagen]
    public IFormFile ImgEvento {get; init;}

    public DateTime FechaEvento { get; init; }

    public Evento(string nombre, string artista, Direccion direccion, int capacidadMaxima, Organizador organizador)
    {
        Artista = artista;
        Nombre = nombre;
        Direccion = direccion;
        CapacidadMaxima = capacidadMaxima;
        Entradas = new Dictionary<string,Entrada>();
        Organizador = organizador;
    }
    
    public Evento(int idEvento, string nombre, string artista, Direccion direccion, int capacidadMaxima, Organizador organizador, IFormFile imgEvento, DateTime fechaEvento)
    {
        IdEvento = idEvento;
        Artista = artista;
        Nombre = nombre;
        Direccion = direccion;
        CapacidadMaxima = capacidadMaxima;
        Entradas = new Dictionary<string,Entrada>();
        Organizador = organizador;
        ImgEvento = imgEvento;
        FechaEvento = fechaEvento;
    }
    
    public Evento() {}
    

}

