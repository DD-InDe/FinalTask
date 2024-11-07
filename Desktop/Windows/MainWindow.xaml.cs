using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Desktop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void MainFrame_OnNavigated(object sender, NavigationEventArgs e) =>
        BackButton.Visibility = MainFrame.CanGoBack ? Visibility.Visible : Visibility.Collapsed;

    private void BackButton_OnClick(object sender, RoutedEventArgs e) => MainFrame.GoBack();
}