using Database.Models;

namespace Desktop.Models;

public class ConstructorModel(Module module, List<Employee> employees)
{
    public Employee? Employee { get; set; }
    public List<Employee>? Employees { get; set; } = employees;
    public Module Module { get; set; } = module;
    public bool IsSelected { get; set; } = false;
}