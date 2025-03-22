using System.ComponentModel;

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

        private string _password1;
        public string Password1
        {
            get { return _password1; }
            set
            {
                if (_password1 != value)
                {
                    _password1 = value;
                    OnPropertyChanged(nameof(Password1));
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string ValidatePasswords()
        {
            return Password1.Equals(Password2) ? "valid" : "invalid";
        }
    }
}
