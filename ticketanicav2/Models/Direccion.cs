namespace ticketanicav2.Models;

public class Direccion
{
    public Direccion(string nombreCiudad, string nombreCalle, int numeroCalle, string nombreLocal)
    {
        NombreCiudad = nombreCiudad;
        NombreCalle = nombreCalle;
        NumeroCalle = numeroCalle;
        NombreLocal = nombreLocal;
    }

    private string NombreCiudad { get; set; }

    private string NombreCalle { get; set; }

    private int? NumeroCalle { get; set; }

    private string NombreLocal { get; set; }

}