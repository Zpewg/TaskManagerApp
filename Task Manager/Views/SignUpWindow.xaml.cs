using System.Windows;
using System.Windows.Controls;

namespace Task_Manager;

public partial class SignUpWindow : Window
{
    public SignUpWindow()
    {
        InitializeComponent();
        this.DataContext = new SignUpViewModel();
    }
    
    private void SignUpButton_Click(object sender, RoutedEventArgs e)
    {
        var viewModel = (SignUpViewModel)this.DataContext;

        if (viewModel == null)
        {
            MessageBox.Show("Unexpected error: ViewModel is null!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        // Verifică fiecare câmp
        viewModel.ValidateUsername();
        viewModel.ValidateEmail();
        viewModel.ValidatePhoneNumber();
        viewModel.ValidatePasswords();

        // Verifică dacă există erori
        if (viewModel.HasErrors)
        {
            MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        // Validarea parolelor
        string passwordValidation = viewModel.ValidatePasswords();
        if (passwordValidation == "valid")
        {
            MessageBox.Show("Sign-up successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            // Aici poți apela service-ul care adaugă user-ul în baza de date
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