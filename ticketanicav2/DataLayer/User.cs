﻿using System;
using System.Collections.Generic;

namespace ticketanicav2.DataLayer;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<ResetToken> ResetTokens { get; } = new List<ResetToken>();
}