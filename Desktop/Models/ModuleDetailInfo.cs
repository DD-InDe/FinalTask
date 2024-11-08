using Database.Models;

namespace Desktop.Models;

public class ModuleDetailInfo
{
    public Module Module { get; set; } = default!;
    public List<Position> Positions { get; set; } = new (); 
    public List<Employee> Developers { get; set; }  = new ();
    public List<Employee> Accessors { get; set; } = new (); 
}