using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Database.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? DepartmentId { get; set; }

    public int? PositionId { get; set; }

    [JsonIgnore] public virtual ICollection<AdaptationProgramModule> AdaptationProgramModules { get; set; } = new List<AdaptationProgramModule>();

    [JsonIgnore] public virtual ICollection<AdaptationProgram> AdaptationPrograms { get; set; } = new List<AdaptationProgram>();

    [JsonIgnore] public virtual ICollection<Collaboration> Collaborations { get; set; } = new List<Collaboration>();

    public virtual Department? Department { get; set; }

    [JsonIgnore] public virtual ICollection<DownloadHistory> DownloadHistories { get; set; } = new List<DownloadHistory>();

    [JsonIgnore] public virtual ICollection<EmployeeQuestionResult> EmployeeQuestionResults { get; set; } = new List<EmployeeQuestionResult>();

    [JsonIgnore] public virtual ICollection<ModuleEditHistory> ModuleEditHistories { get; set; } = new List<ModuleEditHistory>();

    [JsonIgnore] public virtual ICollection<Module> Modules { get; set; } = new List<Module>();

    [JsonIgnore] public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Position? Position { get; set; }

    [JsonIgnore] public virtual ICollection<TestingResult> TestingResults { get; set; } = new List<TestingResult>();
}
