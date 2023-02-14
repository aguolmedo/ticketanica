using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ticketanica.DataLayer;

public partial class ResetToken
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string UserEmail { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public virtual User UserEmailNavigation { get; set; } = null!;
}
