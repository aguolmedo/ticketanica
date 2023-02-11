using System;
using System.Collections.Generic;

namespace ticketanicav2.DataLayer;

public partial class Evento
{
    public int IdEvento { get; set; }

    public string EventoName { get; set; } = null!;

    public int? IdEntrada { get; set; }

    public string? ArtistaName { get; set; }

    public int IdDireccion { get; set; }

    public int? CapacidadMaxima { get; set; }

    public int Organizador { get; set; }

    public virtual Direccione IdDireccionNavigation { get; set; } = null!;

    public virtual Entrada? IdEntradaNavigation { get; set; }

    public virtual User OrganizadorNavigation { get; set; } = null!;
}
