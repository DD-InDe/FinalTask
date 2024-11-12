using Database.Models;

namespace Database.APIModels;

public class AdaptationMapModel
{
    public int AdaptationProgramId { get; set; }
    public List<ModuleMobileModel> Modules { get; set; } = new();
}