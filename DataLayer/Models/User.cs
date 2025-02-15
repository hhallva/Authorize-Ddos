using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int FailedAttempts { get; set; }

    public DateTime? LockTime { get; set; }
}
