﻿using System.ComponentModel;
using System.Windows;
using Task_Manager.Entities;
using Task_Manager.Service;
using Task_Manager.Validations;

namespace Task_Manager;

public class RecoverAccountViewModel
{
    private string _email;
    private readonly UserService _userService;
    private User _user;
    private string _username;
    private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

    public RecoverAccountViewModel(UserService userService, User user)
    {
        _userService = userService;
        _user = user;
    }
   
    public string Email
    {
        get { return _email; }
        set
        {
            if (_email != value)
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
            
    }

    public string Username
    {
        get { return _username; }
        set
        {
            if (_username != value)
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
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
        if (string.IsNullOrEmpty(Email))
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
        if (string.IsNullOrEmpty(Username))
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

    public async Task<bool> RecoverAccount()
    {
        ValidateEmail();
        if (HasErrors)
        {
            MessageBox.Show("All fields must be filled.");
        }
        string errorMessage = await _userService.checkForUserMail(Email);
        if (errorMessage == "Email doesn't exist")
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }
        return true;

    }
    
}