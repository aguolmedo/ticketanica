using System;
using System.Collections.Generic;

namespace ticketanica.DataLayer;

public partial class User
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Evento> Eventos { get; } = new List<Evento>();

    public virtual ICollection<ResetToken> ResetTokens { get; } = new List<ResetToken>();
}
