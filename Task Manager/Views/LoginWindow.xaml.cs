using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Task_Manager.Entities;
using Task_Manager.Repository;
using Task_Manager.Service;
using Task_Manager.Views;

namespace Task_Manager;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
        var userService = App.ServiceProvider.GetRequiredService< UserService >() ;
        var userRepository = App.ServiceProvider.GetRequiredService< UserRepository >();
        this.DataContext = new LoginViewModel(userService, userRepository);
    }

    private async void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        var viewModel = (LoginViewModel)this.DataContext;
        
        User user = await viewModel.LoginUser();
       
        if (user != null)
        {
            MessageBox.Show("User logged in.", "Logged in", MessageBoxButton.OK, MessageBoxImage.Information);
            var tasksWindow = new TasksWindow(user);
            tasksWindow.Show();
            this.Close();
        }
    }
    

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var mainWindow = new MainWindow();
        mainWindow.Show();
        this.Close();
    }
}