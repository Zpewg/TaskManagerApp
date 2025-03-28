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
        
        if (viewModel == null)
        {
            MessageBox.Show("Unexpected error: ViewModel is null!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

       
        viewModel.ValidateEmail();
        viewModel.ValidatePassword();
        
        
        if (viewModel.HasErrors)
        {
            MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        User user = await viewModel.LoginUser();
       
        if (user != null)
        {
            MessageBox.Show("User logged in.", "Logged in", MessageBoxButton.OK, MessageBoxImage.Information);
            var tasksWindow = new TasksWindow(user);
            tasksWindow.Show();
            this.Close();
        }

        if (user is null)
        {
            MessageBox.Show("Unexpected error: User is null!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }


    private void RecoverAccountButton_Click(object sender, RoutedEventArgs e)
    {
        RecoverAccountWindow recoverAccountWindow = new();
        recoverAccountWindow.Show();
    }
}