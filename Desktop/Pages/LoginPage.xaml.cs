using System.Windows;
using System.Windows.Controls;
using Database.APIModels;
using Database.Models;
using Database.Services;
using Desktop.Services;

namespace Desktop.Pages;

public partial class LoginPage : Page
{
    public LoginPage()
    {
        App.LogOutEmployee();
        InitializeComponent();
    }

    private async void LoginButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;
            if (!String.IsNullOrEmpty(login) && !String.IsNullOrEmpty(password))
            {
                LogInModel model = new LogInModel(login, password);
                Employee? employee = await AuthorizationService.LogIn(model);

                if (employee != null)
                {
                    if (employee.PositionId == 14 || employee.DepartmentId == 4)
                    {
                        App.LogInEmployee(employee);
                        NavigationService.Navigate(new MainMenuPage());
                    }
                    else
                    MessageService.ShowInfo("У вас нет доступа");
                }
                else
                    MessageService.ShowInfo("Пользователь не найден");
            }
            else
                MessageService.ShowInfo("Заполните все поля");
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageService.ShowError(exception);
        }
    }
}