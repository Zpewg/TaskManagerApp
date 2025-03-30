using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using Task_Manager.Service;

namespace Task_Manager;

public partial class SignUpWindow : Window
{
    public SignUpWindow()
    {
        InitializeComponent();
    
        var userService = App.ServiceProvider.GetService<UserService>();
            this.DataContext = new SignUpViewModel(userService);
        

    }

    
    private async void SignUpButton_Click(object sender, RoutedEventArgs e)
    {
        var viewModel = (SignUpViewModel)this.DataContext;

        viewModel.RegisterUser();
        
        
    }
    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        var textBox = sender as TextBox;
        var bindingExpression = textBox?.GetBindingExpression(TextBox.TextProperty);
        bindingExpression?.UpdateSource();
    }




}