using System.Windows;
using System.Windows.Controls;
using Database.Models;
using Desktop.Services;

namespace Desktop.Pages;

public partial class AddModulePage : Page
{
    private Module _module = new Module()
    {
        DateCreate = DateTime.Now,
        StatusId = 1
    };
    public AddModulePage()
    {
        InitializeComponent();
    }

    private void AddModulePage_OnLoaded(object sender, RoutedEventArgs e)
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