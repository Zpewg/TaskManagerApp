using System.ComponentModel;
using System.Windows;
using Task_Manager.Entities;
using Task_Manager.Service;

namespace Task_Manager;

public class ChangeUsernameOrEmail
{
    private string _email;
    private string _username;
    
    private readonly UserService _userService;
    private User _user;
    
    private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

    public ChangeUsernameOrEmail(UserService userService, User user)
    {
        _userService = userService;
        _user = user;
    }
        public string ChangedEmail
    {
        get => _email;
        set
        {
                _email = value;
                OnPropertyChanged(nameof(ChangedEmail));
        }
            
    }

    public string ChangedUsername
    {
        get => _username; 
        set
        {
                _username = value;
                OnPropertyChanged(nameof(ChangedUsername));
            
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;
    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    public bool HasErrors => _errors.Any();

    private void AddError(string propertyName, string errorMessage)
    {
        if (!_errors.ContainsKey(propertyName))
        {
            _errors[propertyName] = new List<string>();
        }

        _errors[propertyName].Add(errorMessage);
        OnErrorsChanged(propertyName);
    }

    private void RemoveError(string propertyName)
    {
        if (_errors.ContainsKey(propertyName))
        {
            _errors[propertyName].Clear();
                
            if (_errors[propertyName].Count == 0)
            {
                _errors.Remove(propertyName);
            }

            OnErrorsChanged(propertyName);
        }
    }


    private void OnErrorsChanged(string propertyName)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    public void ValidateEmail()
    {
        if (string.IsNullOrEmpty(ChangedEmail))
        {
            AddError("Email", "Email is required.");
        }
        else
        {
            RemoveError("Email");
        }
    }

    public void ValidateUsername()
    {
      
        if (string.IsNullOrEmpty(ChangedUsername))
        {
            AddError("Username", "Username is required.");
        }
        else
        {
            RemoveError("Username");
        }
    }
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    public async Task ChangeUserName()
    {
        ValidateUsername();
        if (HasErrors)
        {
            MessageBox.Show("All fields must be filled.");
            return;
        }
        await _userService.updateUserName(_user, ChangedUsername);
        OnPropertyChanged(nameof(ChangedUsername));
        MessageBox.Show("User name changed.");
        
    }

    public async Task ChangeEmail()
    {
        ValidateEmail();
        if (HasErrors)
        {
            MessageBox.Show("All fields must be filled.");
            return;
        }

        if (!await _userService.updateUserEmail(_user, ChangedEmail))
        {
            MessageBox.Show("User email not changed.");
            return;
        }
        OnPropertyChanged(nameof(ChangedEmail));
        MessageBox.Show("User email changed.");
    }
}