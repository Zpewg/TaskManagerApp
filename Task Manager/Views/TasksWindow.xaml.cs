using System.Windows;
using System.Windows.Controls;
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
        this.DataContext =new TasksWindowViewModel(userTasksService, user, userTasksRepository);
      
        
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


}
    
