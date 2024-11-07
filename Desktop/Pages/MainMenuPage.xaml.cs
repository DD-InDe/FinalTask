using System.Windows;
using System.Windows.Controls;

namespace Desktop.Pages;

public partial class MainMenuPage : Page
{
    public MainMenuPage()
    {
        InitializeComponent();
    }

    private void ModulesButton_OnClick(object sender, RoutedEventArgs e) =>
        NavigationService.Navigate(new ViewModulesPage());

    private void ConstructorButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void AnalyzeButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
}