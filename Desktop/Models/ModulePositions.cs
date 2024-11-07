using Database.Models;

namespace Desktop.Models;

public class ModulePositions
{
    public Module Module { get; set; }
    public List<Position> Positions { get; set; } 
}