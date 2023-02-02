using System;
using System.Collections.Generic;
using ticketanicav2.Models;

namespace ticketanicav2.DataLayer;

public partial class Direccione
{
    public Direccione(Direccion direccion)
    {
        CalleNro = Convert.ToInt32(direccion.NumeroCalle);
        LocalName = direccion.NombreLocal;
        CalleName = direccion.NombreCalle;
        CiudadName = direccion.NombreCiudad;
    }


    public int IdDireccion { get; set; }

    public int? CalleNro { get; set; }

    public string LocalName { get; set; } = null!;

    public string CalleName { get; set; } = null!;

    public string? CiudadName { get; set; }

    public virtual ICollection<Evento> Eventos { get; } = new List<Evento>();
    
    
}
