using Database.Models;

namespace Database.APIModels;

public class AdaptationMapModel
{
    public int AdaptationProgramId { get; set; }
    public List<Module> Modules { get; set; } = new();
}