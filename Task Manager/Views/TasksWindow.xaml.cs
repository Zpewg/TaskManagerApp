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
        this.DataContext = new TasksWindowViewModel(userTasksService, user, userTasksRepository);
        this.PreviewMouseDown += TasksWindow_PreviewMouseDown;

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

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        EditTaskPopUp.IsOpen = true;
    }

    private async void EditTask_OnClick(object sender, RoutedEventArgs e)
    {
        var viewModel = (TasksWindowViewModel)this.DataContext;
      
        await viewModel.EditTask();
    }

    private void TaskListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (TaskActionPopup == null ) return;

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
                if (!TaskActionPopup.IsOpen && !EditTaskPopUp.IsOpen)
                {
                    listBox.SelectedItem = null; 
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

        await viewModel.DeleteTask();
        
        TaskActionPopup.IsOpen = false;
        
    }

}
    
