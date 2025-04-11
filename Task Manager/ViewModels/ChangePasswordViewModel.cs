using System.ComponentModel;
using System.Windows;
using Task_Manager.Service;

namespace Task_Manager;

public class ChangePasswordViewModel

{
    private string _email;
    private readonly UserService _userService;

    public ChangePasswordViewModel(UserService userService, string email)
    {
        _userService = userService;
        _email = email;
    }
    public event PropertyChangedEventHandler PropertyChanged;
    private string _password;

    public string Password
    {
        get { return _password; }
        set
        {
            if (_password != value)
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
    }

    private string _password2;

    public string Password2
    {
        get { return _password2; }
        set
        {
            if (_password2 != value)
            {
                _password2 = value;
                OnPropertyChanged(nameof(Password2));
            }
        }
    }
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }



    
    public string ValidatePasswords()
    {
        if (Password.Equals(Password2))
        {
            return null;  
        }
        
            return "Passwords do not match!";  
        
    }
    

    public async Task<bool> changeUserPassword()
    {
        string error = ValidatePasswords();
        if (error != null)
        {
           MessageBox.Show(error);
           return false;
        }
        await _userService.updateUserPassword(_email, Password);
            return true;
        
       
    }


}