using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Database.Models;

public partial class Testing
{
    public int Id { get; set; }

    public int? TypeId { get; set; }

    public int? ModuleId { get; set; }

    public virtual Module? Module { get; set; }

    [JsonIgnore] public virtual ICollection<TestQuestion> TestQuestions { get; set; } = new List<TestQuestion>();

    public virtual TestingType? Type { get; set; }
}
