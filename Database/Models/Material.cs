using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Database.Models;

public partial class Material
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public byte[]? FileData { get; set; }

    public int? ModuleId { get; set; }

    public string? FileName { get; set; }

    public virtual Module? Module { get; set; }
}
