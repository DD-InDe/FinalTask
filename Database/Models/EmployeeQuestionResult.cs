using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Database.Models;

public partial class EmployeeQuestionResult
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public int? QuestionId { get; set; }

    public bool? IsCorrect { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual TestQuestion? Question { get; set; }
}
