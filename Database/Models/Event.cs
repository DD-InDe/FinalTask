using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Database.Models;

public partial class Event
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? TypeId { get; set; }

    public int? ModuleId { get; set; }

    public virtual Module? Module { get; set; }

    public virtual EventType? Type { get; set; }
}
