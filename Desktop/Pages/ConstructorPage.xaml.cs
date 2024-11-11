using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using Aspose.Cells;
using Database.Models;
using Database.Services;
using Desktop.Models;
using Desktop.Services;

namespace Desktop.Pages;

public partial class ConstructorPage : Page
{
    private readonly AdaptationProgram _program = new() { DateStart = DateTime.Now };
    private List<ConstructorModel> _constructorModels = new();

    public ConstructorPage()
    {
        InitializeComponent();
        DataContext = _program;
    }

    private async void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (_program is { EmployeeId: not null, PositionId: not null, DepartmentId: not null } &&
                _constructorModels.Any(c => c is { Employee: not null, IsSelected: true }))
            {
                List<AdaptationProgramModule> programModules = new();
                _constructorModels
                    .Where(c => c.IsSelected)
                    .ToList()
                    .ForEach(c =>
                        programModules.Add(new()
                        {
                            AdaptationProgram = _program,
                            ModuleId = c.Module.Id,
                            MentorId = c.Employee.Id
                        }));

                bool isSuccess = await AdaptationManageService.AddAdaptationProgram(programModules);
                if (isSuccess) MessageService.ShowOk("Данные сохранены");
                else MessageService.ShowInfo("Данные не сохранены");

                Workbook workbook = new Workbook();
                Worksheet worksheet = workbook.Worksheets[0];

                worksheet
                    .Cells["A1"]
                    .InsertText(0, "Сотрудник");
                worksheet
                    .Cells["B1"]
                    .InsertText(0, ((Employee)EmployeeComboBox.SelectedItem).FullName);

                worksheet
                    .Cells["A2"]
                    .InsertText(0, "Подразделение");
                worksheet
                    .Cells["B2"]
                    .InsertText(0, ((Department)DepartmentComboBox.SelectedItem).Name);

                worksheet
                    .Cells["A3"]
                    .InsertText(0, "Должность");
                worksheet
                    .Cells["B3"]
                    .InsertText(0, ((Position)PositionComboBox.SelectedItem).Name);

                worksheet
                    .Cells["A4"]
                    .InsertText(0, "Дата начала программы");
                worksheet
                    .Cells["B4"]
                    .InsertText(0, _program.DateStart.Value.ToString("d"));

                worksheet
                    .Cells["A5"]
                    .InsertText(0, "Модули");

                worksheet
                    .Cells["B5"]
                    .InsertText(0, "Модуль");
                worksheet
                    .Cells["C5"]
                    .InsertText(0, "Наставник");

                List<ConstructorModel> temp = _constructorModels
                    .Where(c => c.IsSelected)
                    .ToList();

                for (int i = 0; i < temp.Count; i++)
                {
                    int cellRow = i + 6;

                    worksheet
                        .Cells[$"B{cellRow}"]
                        .InsertText(0, temp[i].Module.Name);
                    worksheet
                        .Cells[$"C{cellRow}"]
                        .InsertText(0, temp[i].Employee.FullName);
                }

                SaveFileDialog dialog = new SaveFileDialog(){DefaultExt = ".xlsx"};
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    workbook.Save(dialog.FileName, SaveFormat.Xlsx);
                }
            }
            else
                MessageService.ShowInfo("Заполните все поля и выберите хотя бы один модуль с указанием наставника!");
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
            List<Employee> employees = await CompanyService.GetEmployeesList() ?? new();
            _constructorModels = new List<ConstructorModel>();
            List<Module> modules = await AdaptationManageService.GetModulesByPosition(_program.PositionId.Value);
            modules.ForEach(c => _constructorModels.Add(new(c, employees)));
            ModuleItems.ItemsSource = _constructorModels;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageService.ShowError(exception);
        }
    }
}