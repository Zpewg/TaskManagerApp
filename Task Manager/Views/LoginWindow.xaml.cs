using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Task_Manager.Service;
using Task_Manager.Views;

namespace Task_Manager;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
        var userService = App.ServiceProvider.GetRequiredService< UserService >() ;
        this.DataContext = new LoginViewModel(userService);
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

        await viewModel.LoginUser();
    }


    private void RecoverAccountButton_Click(object sender, RoutedEventArgs e)
    {
        RecoverAccountWindow recoverAccountWindow = new();
        recoverAccountWindow.Show();
    }
}