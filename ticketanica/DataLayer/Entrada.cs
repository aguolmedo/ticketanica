using System;
using System.Collections.Generic;

namespace ticketanica.DataLayer;

public partial class Entrada
{
    public int IdEntrada { get; set; }

    public string? CodigoQr { get; set; }

    public bool Usada { get; set; }

    public int EventoId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Evento Evento { get; set; } = null!;
}
