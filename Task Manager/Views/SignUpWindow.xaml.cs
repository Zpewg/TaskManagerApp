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

        if (viewModel == null)
        {
            MessageBox.Show("Unexpected error: ViewModel is null!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        
        viewModel.ValidateUsername();
        viewModel.ValidateEmail();
        viewModel.ValidatePhoneNumber();
        viewModel.ValidatePasswords();

        
        if (viewModel.HasErrors)
        {
            MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        
        string passwordValidation = viewModel.ValidatePasswords();
        if (passwordValidation == "valid")
        {
          await viewModel.RegisterUser();
        }
        else
        {
            MessageBox.Show(passwordValidation, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        
    }
    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        var textBox = sender as TextBox;
        var bindingExpression = textBox?.GetBindingExpression(TextBox.TextProperty);
        bindingExpression?.UpdateSource();
    }




}