using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ticketanica.DataLayer;

public partial class Entrada
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdEntrada { get; set; }

    public string? CodigoQr { get; set; } = null!;

    public bool Usada { get; set; }

    public int EventoId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Evento Evento { get; set; } = null!;
}
