using System.Windows;
using System.Windows.Controls;
using Database.Models;
using Database.Services;
using Desktop.Models;
using Desktop.Services;

namespace Desktop.Pages;

public partial class ViewModulesPage : Page
{
    private int _position = 0;

    public ViewModulesPage()
    {
        InitializeComponent();
    }

    private async void ViewModulesPage_OnInitialized(object? sender, EventArgs e)
    {
        try
        {
            List<Position> positions = new() { new() { Id = 0, Name = "Все" } };
            positions.AddRange(await CompanyService.GetPositionsList() ?? new());
            await LoadData();

            PositionComboBox.ItemsSource = positions;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageService.ShowError(exception);
        }
    }

    private async Task LoadData()
    {
        try
        {
            List<ModulePositions> modulePositionsList = new();
            List<Module> modules = await FormationAdaptionProgramService.GetModulesList() ?? new();

            foreach (var module in modules)
            {
                List<Position> positions =
                    await FormationAdaptionProgramService.GetPositionsIncludedInModule(module.Id) ?? new();
                ModulePositions modulePositions = new ModulePositions()
                {
                    Module = module,
                    Positions = positions
                };
                modulePositionsList.Add(modulePositions);
            }

            if (_position != 0)
                modulePositionsList = modulePositionsList.Where(c => c.Positions.Any(p => p.Id == _position))
                    .ToList();

            ModulesDataGrid.ItemsSource = modulePositionsList;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageService.ShowError(exception);
        }
    }

    private async void PositionComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _position = ((Position)PositionComboBox.SelectedItem).Id;
        await LoadData();
    }

    private void AddNewButton_OnClick(object sender, RoutedEventArgs e) =>
        NavigationService.Navigate(new AddModulePage());
}