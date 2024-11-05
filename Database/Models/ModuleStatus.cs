using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Database.Models;

public partial class ModuleStatus
{
    public int Id { get; set; }

    public string? Name { get; set; }

    [JsonIgnore]  public virtual ICollection<Module> Modules { get; set; } = new List<Module>();
}
