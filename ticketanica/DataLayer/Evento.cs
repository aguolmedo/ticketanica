using System;
using System.Collections.Generic;

namespace ticketanica.DataLayer;

public partial class Evento
{
    public int IdEvento { get; set; }

    public string EventoName { get; set; } = null!;

    public string? ArtistaName { get; set; }

    public int IdDireccion { get; set; }

    public int CapacidadMaxima { get; set; }

    public string? EmailOrganizador { get; set; }

    public DateTime EventoFecha { get; set; }

    public string? EventoImg { get; set; }

    public virtual User? EmailOrganizadorNavigation { get; set; }

    public virtual ICollection<Entrada> Entrada { get; } = new List<Entrada>();

    public virtual Direccione IdDireccionNavigation { get; set; } = null!;
}
