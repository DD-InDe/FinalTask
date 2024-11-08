using System.Collections.ObjectModel;
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

    private Module _module = new ()
    {
        DateCreate = DateTime.Now,
        StatusId = 1,
        ModuleDevelopDeadline = 0
    };

    public AddModulePage()
    {
        InitializeComponent();
        DataContext = _module;
    }

    /// <summary>
    /// Метод обрабатывающий собтие загрузки страницы.
    /// Используется для загрузки данных с сервера на страницу
    /// </summary>
    /// <param name="sender">Элемент, вызывающий событие</param>
    /// <param name="e">Событие</param>
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

    /// <summary>
    /// Метод обрабатывающий событие нажатие кнопки.
    /// Используется для сохранения данных
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            string daysText = DeadlineTextBox.Text;
            int days = 0;
            bool isOk = int.TryParse(daysText, out days);
            if (!isOk)
            {
                MessageService.ShowInfo("Неверный формат в поле \"Срок разработки\"");
                return;
            }

            if (days > 0 && !String.IsNullOrEmpty(_module.CodeName)
                         && DevelopersComboBox.SelectedItems.Count != 0
                         && AccessorsComboBox.SelectedItems.Count != 0
                         && MainAccessorsComboBox.SelectedItem != null)
            {
                List<object> developers = ((ObservableCollection<object>)DevelopersComboBox.SelectedItems).ToList();
                List<object> accessors = ((ObservableCollection<object>)AccessorsComboBox.SelectedItems).ToList();
                Employee mainAccessor = MainAccessorsComboBox.SelectedItem as Employee ?? default!;
                List<Collaboration> collaborations = new List<Collaboration>();

                foreach (var developer in developers)
                {
                    collaborations.Add(new()
                    {
                        EmployeeId = ((Employee)developer).Id,
                        RoleId = 1,
                        IsAccepted = false,
                        IsMain = false,
                        Module = _module
                    });
                }

                foreach (var accessor in accessors)
                {
                    collaborations.Add(new()
                    {
                        EmployeeId = ((Employee)accessor).Id,
                        RoleId = 2,
                        IsAccepted = false,
                        IsMain = accessor == mainAccessor,
                        Module = _module
                    });
                }

                bool isSuccess = await AdaptationManageService.AddModuleAndCollaborations(collaborations);
                MessageService.ShowOk(isSuccess.ToString());
            }
            else
            {
                MessageService.ShowInfo("Все поля обязательны для заполнения. Срок разработки должен быть больше 0.");
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageService.ShowError(exception);
        }
    }

    private void AccessorsComboBox_OnItemSelectionChanged(object sender, ItemSelectionChangedEventArgs e) =>
        MainAccessorsComboBox.ItemsSource = AccessorsComboBox.SelectedItems;
}