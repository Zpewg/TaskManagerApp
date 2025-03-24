using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Task_Manager
{
    public class SignUpViewModel : INotifyPropertyChanged
    {
        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                    ValidateUsername();
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
                    ValidateEmail();
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
                    ValidatePhoneNumber();
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

        public IEnumerable<string> GetErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                return _errors[propertyName];
            }
            return null;
        }

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
    }
}
