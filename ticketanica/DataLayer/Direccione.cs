using System;
using System.Collections.Generic;

namespace ticketanicav2.DataLayer;

public partial class Direccione
{
    public int IdDireccion { get; set; }

    public int? CalleNro { get; set; }

    public string LocalName { get; set; } = null!;

    public string CalleName { get; set; } = null!;

    public string? CiudadName { get; set; }

    public virtual ICollection<Evento> Eventos { get; } = new List<Evento>();
}
