using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Database.Models;

public partial class ModuleEditHistory
{
    public int Id { get; set; }

    public int ModuleId { get; set; }

    public int EmployeeId { get; set; }

    public DateTime? Datetime { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Module Module { get; set; } = null!;
}
