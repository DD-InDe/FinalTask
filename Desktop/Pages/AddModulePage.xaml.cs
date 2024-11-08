using System.Windows;
using System.Windows.Controls;
using Database.Models;
using Database.Services;
using Desktop.Services;
using Xceed.Wpf.Toolkit.Primitives;

namespace Desktop.Pages;

public partial class AddModulePage : Page
{
    private List<Employee> _allEmployees = new();
    private List<Employee> _selectedDevelopers = new();
    private List<Employee> _selectedAccessors = new();

    private Module _module = new Module()
    {
        DateCreate = DateTime.Now,
        StatusId = 1
    };

    public AddModulePage()
    {
        InitializeComponent();
    }

    private async void AddModulePage_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            _allEmployees = await CompanyService.GetEmployeesList() ?? new();
            DevelopersComboBox.ItemsSource = _allEmployees;
            AccessorsComboBox.ItemsSource = _allEmployees;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageService.ShowError(exception);
        }
    }

    private void DevelopersComboBox_OnItemSelectionChanged(object sender, ItemSelectionChangedEventArgs e)
    {
        try
        {
            _selectedDevelopers = DevelopersComboBox.SelectedItems as List<Employee>;
            AccessorsComboBox.ItemsSource = _allEmployees
                .Where(c => !_selectedDevelopers.Contains(c))
                .ToList();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageService.ShowError(exception);
        }
    }

    private void AccessorsComboBox_OnItemSelectionChanged(object sender, ItemSelectionChangedEventArgs e)
    {
        try
        {
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageService.ShowError(exception);
        }
    }

    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageService.ShowError(exception);
        }
    }
}