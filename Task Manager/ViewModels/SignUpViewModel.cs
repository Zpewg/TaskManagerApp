using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Task_Manager.Entities;
using Task_Manager.Service;

namespace Task_Manager
{
    public class SignUpViewModel : INotifyPropertyChanged
    {
        private string _username;
        private UserService _userService;

        public SignUpViewModel(UserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
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

        private string _email;

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

        private string _phoneNumber;

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged(nameof(PhoneNumber));
                }
            }
        }

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

        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public event PropertyChangedEventHandler PropertyChanged;

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

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

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

        public void ValidatePhoneNumber()
        {
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                AddError("PhoneNumber", "Phone number is required.");
            }
            else
            {
                RemoveError("PhoneNumber");
            }
        }

        public string ValidatePasswords()
        {
            return Password.Equals(Password2) ? "valid" : "Passwords do not match!";
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task RegisterUser()
        {
            
            ValidateUsername();
            ValidateEmail();
            ValidatePhoneNumber();
            if (HasErrors )
            {
                MessageBox.Show("All fields must be filled", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ValidatePasswords() != "valid")
            {
                MessageBox.Show("Passwords do not match", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            User newUser =  new User(Username, Email, Password, PhoneNumber);
            Console.WriteLine(newUser.ToString());
            List<string> errorsFromService = await _userService.createUser(newUser);
            if (errorsFromService.Any())
            {
                string errorMessage = string.Join("\n", errorsFromService);
                MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                errorsFromService.Clear();
            }
            else
            {
                MessageBox.Show("User created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }

}
    

