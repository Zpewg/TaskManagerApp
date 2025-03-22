using System.Windows;

namespace Task_Manager;

public partial class SignUpWindow : Window
{
    public SignUpWindow()
    {
        InitializeComponent();
    }

    private string passwordIsTheSame(string pass1, string pass2)
    {
        if (pass1.Equals(pass2))
            return "valid";
        return "invalid";
    }
    private void SignUpButton_Click(object sender, RoutedEventArgs e)
    {
        var viewModel = (SignUpViewModel) this.DataContext;
        
        
    }
}