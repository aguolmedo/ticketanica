namespace ticketanicav2.Models;

public class Evento
{
    public int? IdEvento { get; set; }
    public string Nombre { get; set; }

    public string Artista { get; set; }
    public Dictionary<string,Entrada>? Entradas { get; set; }

    public Direccion Direccion { get; set; }

    public int CapacidadMaxima { get; set; }

    public Organizador? Organizador { get; set; }
    
    public IFormFile ImgEvento {get; set;} 

    public Evento(string nombre, string artista, Direccion direccion, int capacidadMaxima, Organizador organizador)
    {
        Artista = artista;
        Nombre = nombre;
        Direccion = direccion;
        CapacidadMaxima = capacidadMaxima;
        Entradas = new Dictionary<string,Entrada>();
        Organizador = organizador;
    }
    
    public Evento(int idEvento, string nombre, string artista, Direccion direccion, int capacidadMaxima, Organizador organizador, IFormFile imgEvento)
    {
        IdEvento = idEvento;
        Artista = artista;
        Nombre = nombre;
        Direccion = direccion;
        CapacidadMaxima = capacidadMaxima;
        Entradas = new Dictionary<string,Entrada>();
        Organizador = organizador;
        ImgEvento = imgEvento;
    }
    
    public Evento() {}
    

}

