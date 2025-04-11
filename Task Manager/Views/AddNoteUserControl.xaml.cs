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
    private TaskJournal _editingNote;
    public AddNoteUserControl(User user, TaskJournal? noteToEdit=null)
    {
        InitializeComponent();
        TaskJournalService journalService = App.ServiceProvider.GetRequiredService<TaskJournalService>();
        TaskJournalRepository journalRepository = App.ServiceProvider.GetRequiredService<TaskJournalRepository>();
        _user = user;
        var viewModel = new JournalUserControlViewModel(journalService, user, journalRepository);
        if (noteToEdit != null)
        {
            _editingNote = noteToEdit;
            viewModel.NoteName = noteToEdit.NoteName;
            viewModel.NoteText = noteToEdit.NoteText;
            viewModel.EditingNote = noteToEdit;
            
        }
        this.DataContext = viewModel;
    }
    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        var journal = (JournalUserControlViewModel)this.DataContext;
        await journal.addNote();
        BackButton_Click(sender, e);
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as TasksWindow;
        var userJournal = new JournalUserControl(_user);
        if (parentWindow != null)
        {
            parentWindow.UserSettingsContainer.Content = userJournal;
            parentWindow.UserSettingsContainer.Visibility = Visibility.Visible;

        }
    }
}