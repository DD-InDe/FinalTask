using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Database.Models;

public partial class Collaboration
{
    public int Id { get; set; }

    public bool? IsMain { get; set; }

    public int? ModuleId { get; set; }

    public int? EmployeeId { get; set; }

    public int? RoleId { get; set; }

    public bool? IsAccepted { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Module? Module { get; set; }

    public virtual Role? Role { get; set; }
}
