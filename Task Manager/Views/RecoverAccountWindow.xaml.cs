using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Task_Manager.Entities;
using Task_Manager.Service;


namespace Task_Manager.Views;

public partial class RecoverAccountWindow 
{
    public RecoverAccountWindow(User user)
    {
        InitializeComponent();
        var userService = App.ServiceProvider.GetRequiredService< UserService >() ;
        this.DataContext = new RecoverAccountViewModel(userService, user);
    }



    public async void CheckValidMail_Click(object sender, RoutedEventArgs e)
    {
        
        var viewModel = (RecoverAccountViewModel)this.DataContext;
        var changePasswordWindow = new ChangePasswordWindow(viewModel.Email);
       

        if (await viewModel.RecoverAccount())
        {
            changePasswordWindow.Show();
            this.Close();
        }

      
    }
}