using Database.Models;

namespace Database.APIModels;

public class ModuleMobileModel
{
    public Module Module { get; set; } = default!;
    public Employee Mentor { get; set; } = default!;
    public float Progress { get; set; } = 0;
    public int Rating { get; set; } = 0;
}