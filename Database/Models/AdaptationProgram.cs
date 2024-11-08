using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Database.Models;

public partial class AdaptationProgram
{
    public int Id { get; set; }

    public DateTime? DateStart { get; set; }

    public int? EmployeeId { get; set; }

    public int? DepartmentId { get; set; }

    public int? PositionId { get; set; }

    [JsonIgnore] public virtual ICollection<AdaptationProgramModule> AdaptationProgramModules { get; set; } = new List<AdaptationProgramModule>();

    public virtual Department? Department { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Position? Position { get; set; }
}
