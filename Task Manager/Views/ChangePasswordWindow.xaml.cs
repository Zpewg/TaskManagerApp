using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Task_Manager.Service;

namespace Task_Manager.Views;

public partial class ChangePasswordWindow : Window
{
    public ChangePasswordWindow(string email)
    {
        InitializeComponent();
        var userService = App.ServiceProvider.GetRequiredService<UserService>();
        this.DataContext = new ChangePasswordViewModel(userService, email);
    }

    public async void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
    {
        var mainWindow = new MainWindow();
       
   
        var viewModel = (ChangePasswordViewModel)this.DataContext;
        
        string error = viewModel.ValidatePasswords();

       
        if (error == "Passwords do not match!")
        {
            MessageBox.Show(error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        
        if (await viewModel.changeUserPassword())
        {
            MessageBox.Show("Password changed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            mainWindow.Show();
            this.Close();
        }
    }
}