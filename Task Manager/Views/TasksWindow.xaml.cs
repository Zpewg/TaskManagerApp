using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Task_Manager.Entities;

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
        this.DataContext = user;
    }

}
    
