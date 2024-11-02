using Database.Models;

namespace Web.Services;

public class UserService
{
    private Employee? _employee;

    public void LogIn(Employee employee) => _employee = employee;

    public Employee GetEmployee() => _employee;

    public void LogOut() => _employee = null;

    public bool Authorized() => _employee != null;
}