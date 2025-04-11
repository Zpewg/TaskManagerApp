using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Extensions.DependencyInjection;
using Task_Manager.Entities;
using Task_Manager.Repository;
using Task_Manager.Service;

namespace Task_Manager.Views;

public partial class JournalUserControl 
{
    private User _user;
    public JournalUserControl(User user)
    {
       
        InitializeComponent();
    
        var journalService = App.ServiceProvider.GetRequiredService<TaskJournalService>();

        var journalRepository = App.ServiceProvider.GetRequiredService<TaskJournalRepository>();
        this.DataContext = new JournalUserControlViewModel(journalService, user, journalRepository);
        _user = user;
        this.PreviewMouseDown += TasksWindow_PreviewMouseDown;
       
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

    private async void JournalButton_OnClick(object sender, RoutedEventArgs e)
    {
        var addNoteControl = new AddNoteUserControl(_user);

       
        var parentWindow = Window.GetWindow(this) as TasksWindow;
        if (parentWindow != null)
        {
            parentWindow.UserSettingsContainer.Content = addNoteControl;
        }
    }
    private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (((FrameworkElement)e.OriginalSource).DataContext is TaskJournal selectedNote)
        {
            var editNoteControl = new AddNoteUserControl(_user, selectedNote);
            var parentWindow = Window.GetWindow(this) as TasksWindow;
            if (parentWindow != null)
            {
                parentWindow.UserSettingsContainer.Content = editNoteControl;
            }
        }
    }

    private void NotesListBox_OnMouseRightClick(object sender, MouseButtonEventArgs e)
    {
        if (TaskActionPopup == null) return;
        
        var viewModel = (JournalUserControlViewModel)this.DataContext;
        var listBox = sender as ListBox;
        
        var clickedItem = VisualTreeHelper.HitTest(listBox, e.GetPosition(listBox))?.VisualHit;
        while (clickedItem != null && !(clickedItem is ListBoxItem))
        {
            clickedItem = VisualTreeHelper.GetParent(clickedItem);
        }

        if (clickedItem is ListBoxItem listBoxItem)
        {
            listBoxItem.IsSelected = true;
        }
        
        viewModel.EditingNote =(TaskJournal)listBox.SelectedItem;
        
        var selectedItem = 
            listBox?.ItemContainerGenerator.ContainerFromItem(viewModel.EditingNote) as FrameworkElement;

        if (selectedItem != null)
        {
            TaskActionPopup.PlacementTarget = selectedItem;
            TaskActionPopup.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            
            TaskActionPopup.IsOpen = true;
        }

        TaskActionPopup.Closed += (s, args) =>
        {
            if (!TaskActionPopup.IsOpen)
            {
                listBox.SelectedItem = null;
                viewModel.EditingNote = null;
            }
        };
        Console.WriteLine(viewModel.EditingNote.ToString());

    }
    private void TasksWindow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (TaskActionPopup.IsOpen && !TaskActionPopup.IsMouseOver)
        {
            TaskActionPopup.IsOpen = false;
        }
        
    }

    private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
    {
        var viewModel = (JournalUserControlViewModel)this.DataContext;

        viewModel.Delete();
        TaskActionPopup.IsOpen = false;
    }
}