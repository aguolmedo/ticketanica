using System;
using System.Collections.Generic;

namespace ticketanicav2.DataLayer;

public partial class ResetToken
{
    public int Id { get; set; }

    public string UserEmail { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public virtual User UserEmailNavigation { get; set; } = null!;
}
