namespace ticketanicav2.Models;

public class Evento
{
    public int IdEvento { get; set; }
    public string Nombre { get; set; }

    public string Artista { get; set; }
    public HashSet<Entrada>? Entradas { get; set; }

    public Direccion Direccion { get; set; }

    public int CapacidadMaxima { get; set; }

    public Organizador? Organizador { get; set; }


    public Evento(string nombre, string artista, Direccion direccion, int capacidadMaxima, Organizador organizador)
    {
        Artista = artista;
        Nombre = nombre;
        Direccion = direccion;
        CapacidadMaxima = capacidadMaxima;
        Entradas = new HashSet<Entrada>();
        Organizador = organizador;
    }
    
    public Evento(int idEvento, string nombre, string artista, Direccion direccion, int capacidadMaxima, Organizador organizador)
    {
        IdEvento = idEvento;
        Artista = artista;
        Nombre = nombre;
        Direccion = direccion;
        CapacidadMaxima = capacidadMaxima;
        Entradas = new HashSet<Entrada>();
        Organizador = organizador;
    }

    public Evento() {}

    public Entrada generarEntrada()
    {
        var newEntrada = new Entrada();
        Entradas.Add(newEntrada);
        return newEntrada;
    }


}

