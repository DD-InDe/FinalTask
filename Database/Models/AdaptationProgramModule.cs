using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Database.Models;

public partial class AdaptationProgramModule
{
    public int Id { get; set; }

    public int? AdaptationProgramId { get; set; }

    public int? ModuleId { get; set; }

    public int? MentorId { get; set; }

    public bool? IsComplete { get; set; }

    public bool? IsOpen { get; set; }

    public virtual AdaptationProgram? AdaptationProgram { get; set; }

    public virtual Employee? Mentor { get; set; }

    public virtual Module? Module { get; set; }
}
