using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Database.Models;

public partial class ModuleAccess
{
    public int Id { get; set; }

    public int? ModuleId { get; set; }

    public int? PositionId { get; set; }

    public virtual Module? Module { get; set; }

    public virtual Position? Position { get; set; }
}
