using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using Task_Manager.Entities;
using Task_Manager.Repository;
using Task_Manager.Service;

namespace Task_Manager.Views;

public partial class JournalUserControl : UserControl
{
    private User _user;
    public JournalUserControl(User user)
    {
        InitializeComponent();
        var journalService = App.ServiceProvider.GetRequiredService<TaskJournalService>();
        var journalRepository = App.ServiceProvider.GetRequiredService<TaskJournalRepository>();
        this.DataContext = new JournalUserControlViewModel(journalService, user, journalRepository);
        _user = user;
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

    private void JournalButton_OnClick(object sender, RoutedEventArgs e)
    {
        var addNoteControl = new AddNoteUserControl(_user);

       
        var parentWindow = Window.GetWindow(this) as TasksWindow;
        if (parentWindow != null)
        {
            parentWindow.UserSettingsContainer.Content = addNoteControl;
        }
    }
}