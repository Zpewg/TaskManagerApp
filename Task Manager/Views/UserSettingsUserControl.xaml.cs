using System.Windows;
using System.Windows.Controls;

namespace Task_Manager.Views;

public partial class UserSettingsUserControl : UserControl
{
    public UserSettingsUserControl()
    {
        InitializeComponent();
    }
    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as TasksWindow;
        if (parentWindow != null)
        {
            parentWindow.UserSettingsContainer.Visibility = Visibility.Collapsed;
            parentWindow.UserSettingsContainer.Content = null;
        }
    }
}