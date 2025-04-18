﻿using System.Windows;
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
        
        if (await viewModel.changeUserPassword())
        {
            MessageBox.Show("Password changed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}