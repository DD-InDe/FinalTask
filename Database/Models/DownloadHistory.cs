using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Database.Models;

public partial class DownloadHistory
{
    public int Id { get; set; }

    public int? MaterialId { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Material? Material { get; set; }
}
