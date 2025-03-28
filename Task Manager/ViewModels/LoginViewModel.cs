using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Task_Manager.Entities;
using Task_Manager.Repository;
using Task_Manager.Service;

namespace Task_Manager
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _email;
        private string _password;
        private readonly UserService _userService;
        private readonly UserRepository _userRepository;
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public LoginViewModel(UserService userService, UserRepository userRepository)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
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
            Console.WriteLine($"Email validation - Errors: {string.Join(", ", _errors.ContainsKey("Email") ? _errors["Email"] : new List<string>())}");
        }

        public void ValidatePassword()
        {
            if (string.IsNullOrEmpty(Password))
            {
                AddError("Password", "Password is required.");
            }
            else
            {
                RemoveError("Password");
            }
            Console.WriteLine($"Password validation - Errors: {string.Join(", ", _errors.ContainsKey("Password") ? _errors["Password"] : new List<string>())}");
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        

        public async Task<User> LoginUser()
        {  
            
            string result = await _userService.loginUser(Email, Password);
            if (result == "User successfully logged in")
            {
                MessageBox.Show("Welcome!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                User user = await _userRepository.ReturnUser(Email);
                return user;
            }
           
            MessageBox.Show(result, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return null;
        }

    }
}
