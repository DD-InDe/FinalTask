using System.Windows;
using System.Windows.Controls;
using Database.Models;
using Database.Services;
using Desktop.Services;

namespace Desktop.Pages;

public partial class ConstructorPage : Page
{
    private readonly AdaptationProgram _program = new ();
    private List<ModuleAccess> _modules;
    
    public ConstructorPage()
    {
        InitializeComponent();
        DataContext = _program;
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

    private async void ConstructorPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            EmployeeComboBox.ItemsSource = await CompanyService.GetEmployeesList();
            DepartmentComboBox.ItemsSource = await CompanyService.GetDepartmentsList();
            PositionComboBox.ItemsSource = await CompanyService.GetPositionsList();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageService.ShowError(exception);
        }
    }

    private async void PositionComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            ModuleItems.ItemsSource = await AdaptationManageService.GetModulesByPosition(_program.PositionId.Value);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageService.ShowError(exception);
        }
    }
}