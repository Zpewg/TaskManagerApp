using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Extensions.DependencyInjection;
using Task_Manager.Entities;
using Task_Manager.Repository;
using Task_Manager.Service;


namespace Task_Manager.Views;

public partial class TasksWindow : Window
{

    private User _loggedUser;
    private NotificationService _notificationService;
    public TasksWindow(User user)
    {
        InitializeComponent();
      
        AddTaskButton.Content = new TextBlock
        {
            Text = "Add task +",
            FontSize = 14,
            FontWeight = FontWeights.Bold,
            Foreground = Brushes.Black,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        AddTaskButton.MouseEnter += (s, e) =>
        {
            AddTaskButton.Background = Brushes.LightGray;
            ((TextBlock)AddTaskButton.Content).Foreground = Brushes.DarkBlue;
        };

        AddTaskButton.MouseLeave += (s, e) =>
        {
            AddTaskButton.Background = Brushes.White;
            ((TextBlock)AddTaskButton.Content).Foreground = Brushes.Black;
        };
        var userTasksService = App.ServiceProvider.GetRequiredService<UserTasksService>();
        var userTasksRepository = App.ServiceProvider.GetRequiredService<UserTasksRepository>();
        this.DataContext =new TasksWindowViewModel(userTasksService, user, userTasksRepository);
        this.PreviewMouseDown += TasksWindow_PreviewMouseDown;
        _notificationService = new NotificationService(userTasksRepository, user);
      
        _loggedUser = user;
    }

    private void AddTaskButton_OnClick(object sender, RoutedEventArgs e)
    {
        AddTaskPopup.IsOpen = true;
    }

    private async void SaveTaskButton_Click(object sender, RoutedEventArgs e)
    {
        var viewModel = (TasksWindowViewModel)this.DataContext;
        await viewModel.CreateTask();
        AddTaskPopup.IsOpen = false;
    }

    private  void EditButton_Click(object sender, RoutedEventArgs e)
    {
        EditTaskPopup.IsOpen = true;
    }

    private async void EditButton_OnClick(object sender, RoutedEventArgs e)
    {
        var viewModel = (TasksWindowViewModel)this.DataContext;
         await viewModel.EditTask();
         EditTaskPopup.IsOpen = false;
         
    }
    private void TaskListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (TaskActionPopup == null || e.AddedItems.Count == 0) return;

        var viewModel = (TasksWindowViewModel)this.DataContext;
        var listBox = sender as ListBox;

     
        viewModel.SelectedTask = (UserTasks)listBox.SelectedItem;

        var selectedItem =
            listBox?.ItemContainerGenerator.ContainerFromItem(viewModel.SelectedTask) as FrameworkElement;

        if (selectedItem != null)
        {
          
            TaskActionPopup.PlacementTarget = selectedItem;
            TaskActionPopup.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;

            
            TaskActionPopup.IsOpen = true;
        }

        
        TaskActionPopup.Closed += (s, args) =>
        {
            if (!TaskActionPopup.IsOpen && !EditTaskPopup.IsOpen)
            {
                listBox.SelectedItem = null; 
                viewModel.SelectedTask = null;
            }
        };
        
        
    }
    private void TasksWindow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (TaskActionPopup.IsOpen && !TaskActionPopup.IsMouseOver)
        {
            TaskActionPopup.IsOpen = false;
        }

        if (AddTaskPopup.IsOpen && !AddTaskPopup.IsMouseOver)
        {
            AddTaskPopup.IsOpen = false;
        }
    }

    
    private async void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
    {
        var viewModel = (TasksWindowViewModel)this.DataContext;
        Console.WriteLine(viewModel.SelectedTask.ToString());
        await viewModel.DeleteTask();
        EditTaskPopup.IsOpen = false;
    }

    private void LogOutButton_OnClick(object sender, RoutedEventArgs e)
    {  var mainWindow = new MainWindow();
        mainWindow.Show();
        this.Close();
        
    }

    public void UserSettingsButton_Click(object sender, RoutedEventArgs e)
    {
        var userSettings = new UserSettingsUserControl(_loggedUser);

        UserSettingsContainer.Content = userSettings;
        UserSettingsContainer.Visibility = Visibility.Visible;
    }

    public void UserJournalButton_Click(object sender, RoutedEventArgs e)
    {
        var userJournal = new JournalUserControl(_loggedUser);

        UserSettingsContainer.Content = userJournal;
        UserSettingsContainer.Visibility = Visibility.Visible;
        
        
    }
}
    
