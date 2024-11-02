using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Database.Models;

public partial class TestQuestion
{
    public int Id { get; set; }

    public string? Question { get; set; }

    public string? Answer { get; set; }

    public int? TestingId { get; set; }

    [JsonIgnore] public virtual ICollection<EmployeeQuestionResult> EmployeeQuestionResults { get; set; } = new List<EmployeeQuestionResult>();

    public virtual Testing? Testing { get; set; }
}
