using System.ComponentModel.DataAnnotations;

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

    [MaxLength(24)]
    public string NombreCiudad { get; init; }
    
    [MaxLength(24)]
    public string NombreCalle { get; init; }

    [Range(0, int.MaxValue, ErrorMessage = "Debe ser un numero positivo. > 0 .")]
    public int? NumeroCalle { get; init; }

    [MaxLength(24)]
    public string NombreLocal { get; init; }
    
    

}