using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Database.Models;

public partial class TestingResult
{
    public int Id { get; set; }

    public int? TestingId { get; set; }

    public bool? IsComplete { get; set; }

    public int? CorrectAnswers { get; set; }

    public virtual Testing? Testing { get; set; }
}
