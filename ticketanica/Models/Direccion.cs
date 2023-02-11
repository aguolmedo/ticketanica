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

    public Direccion() {}

    public string NombreCiudad { get; set; }

    public string NombreCalle { get; set; }

    public int? NumeroCalle { get; set; }

    public string NombreLocal { get; set; }
    
    

}