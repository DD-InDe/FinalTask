using System.Windows;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;

namespace Desktop.Services;

public abstract class MessageService
{
    public static void ShowOk(string message) =>
        MessageBox.Show(message, "Сообщение", MessageBoxButton.OK, MessageBoxImage.Asterisk);

    public static void ShowError(Exception exception) =>
        MessageBox.Show($"Прозошла ошибка: {exception.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

    public static void ShowInfo(string message) =>
        MessageBox.Show(message, "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
}