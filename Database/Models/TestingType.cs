using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Database.Models;

public partial class TestingType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    [JsonIgnore] public virtual ICollection<Testing> Testings { get; set; } = new List<Testing>();
}
