using System;
using System.Collections.Generic;

namespace ticketanicav2.DataLayer;

public partial class Entrada
{
    public int IdEntrada { get; set; }

    public string CodigoQr { get; set; } = null!;

    public bool Usada { get; set; }

    public virtual ICollection<Evento> Eventos { get; } = new List<Evento>();
}
