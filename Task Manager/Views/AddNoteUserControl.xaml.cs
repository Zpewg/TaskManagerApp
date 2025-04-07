using System.Windows;
using System.Windows.Controls;
using Task_Manager.Entities;

namespace Task_Manager.Views;

public partial class AddNoteUserControl : UserControl
{
    private User _user;
    public AddNoteUserControl(User user)
    {
        InitializeComponent();
        _user = user;
        
    }
    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        string title = TitleBox.Text;
        string content = ContentBox.Text;

       
        MessageBox.Show($"Note saved for {_user.name}:\nTitle: {title}");
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