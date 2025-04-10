using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using Task_Manager.Entities;
using Task_Manager.Repository;
using Task_Manager.Service;

namespace Task_Manager.Views;

public partial class AddNoteUserControl : UserControl
{
    private User _user;
    public AddNoteUserControl(User user)
    {
        InitializeComponent();
        TaskJournalService journalService = App.ServiceProvider.GetRequiredService<TaskJournalService>();
        TaskJournalRepository journalRepository = App.ServiceProvider.GetRequiredService<TaskJournalRepository>();
        _user = user;
        this.DataContext = new JournalUserControlViewModel(journalService, user, journalRepository);
        
    }
    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        var journal = (JournalUserControlViewModel)this.DataContext;
        
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