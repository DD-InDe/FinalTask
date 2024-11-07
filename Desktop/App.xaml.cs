using System.Configuration;
using System.Data;
using System.Windows;
using Database.Models;

namespace Desktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static Employee _employee;
    public static Employee GetEmployee() => _employee;
    public static void LogInEmployee(Employee employee) => _employee = employee;
    public static void LogOutEmployee() => _employee = null;
}