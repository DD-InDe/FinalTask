using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Database.Models;

public partial class Module
{
    public int Id { get; set; }

    public string CodeName { get; set; } = null!;

    public string? Name { get; set; }

    public DateTime? DateCreate { get; set; }

    public int? ModuleCompleteDeadline { get; set; }

    public int? ModuleDevelopDeadline { get; set; }

    public bool? IsReleased { get; set; }

    public int? StatusId { get; set; }

    public int? PreviousId { get; set; }

    public int? ResponsiblePersonId { get; set; }

    public string? Source { get; set; } = null!;

    [JsonIgnore]  public virtual ICollection<Collaboration> Collaborations { get; set; } = new List<Collaboration>();

    [JsonIgnore]  public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    [JsonIgnore]  public virtual ICollection<Module> InversePrevious { get; set; } = new List<Module>();

    [JsonIgnore]  public virtual ICollection<Material> Materials { get; set; } = new List<Material>();

    [JsonIgnore]  public virtual ICollection<ModuleAccess> ModuleAccesses { get; set; } = new List<ModuleAccess>();

    [JsonIgnore]  public virtual ICollection<ModuleEditHistory> ModuleEditHistories { get; set; } = new List<ModuleEditHistory>();

    public virtual Module? Previous { get; set; }

    public virtual Employee? ResponsiblePerson { get; set; }

    public virtual ModuleStatus? Status { get; set; }

    [JsonIgnore]  public virtual ICollection<Testing> Testings { get; set; } = new List<Testing>();
}
