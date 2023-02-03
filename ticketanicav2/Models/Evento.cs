namespace ticketanicav2.Models;

public class Evento
{
    private int IdEvento { get; set; }
    private string Nombre { get; set; }

    private string Artista { get; set; }
    private HashSet<Entrada> Entradas { get; set; }

    private Direccion Direccion { get; set; }

    private int CapacidadMaxima { get; set; }


    public Evento(string nombre, string artista, Direccion direccion, int capacidadMaxima)
    {
        Artista = artista;
        Nombre = nombre;
        Direccion = direccion;
        CapacidadMaxima = capacidadMaxima;
        Entradas = new HashSet<Entrada>();
    }
    
    public Evento(int idEvento, string nombre, string artista, Direccion direccion, int capacidadMaxima)
    {
        IdEvento = idEvento;
        Artista = artista;
        Nombre = nombre;
        Direccion = direccion;
        CapacidadMaxima = capacidadMaxima;
        Entradas = new HashSet<Entrada>();
    }

    public Entrada generarEntrada()
    {
        var newEntrada = new Entrada();
        Entradas.Add(newEntrada);
        return newEntrada;
    }


}

