using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Database.Models;

public partial class Department
{
    public int Id { get; set; }

    public string? Name { get; set; }

    [JsonIgnore] public virtual ICollection<AdaptationProgram> AdaptationPrograms { get; set; } = new List<AdaptationProgram>();

    [JsonIgnore] public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
