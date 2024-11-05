using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Database.Models;

public partial class Position
{
    public int Id { get; set; }

    public string? Name { get; set; }

    [JsonIgnore]  public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [JsonIgnore]  public virtual ICollection<ModuleAccess> ModuleAccesses { get; set; } = new List<ModuleAccess>();
}
