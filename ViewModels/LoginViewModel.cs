using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace PhoneBook.ViewModels
{
    class LoginViewModel : ViewModelBase
    {
        private string _username;
        private string _password;
        private bool _isPasswordVisible;
        private string _errorMessage;
        private Color _errorColor;
        
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                ErrorMessage = string.Empty;
                OnPropertyChanged(nameof(Username));
                OnPropertyChanged(nameof(IsCancelEnabled));
                OnPropertyChanged(nameof(IsLoginEnabled));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                ErrorMessage = string.Empty;
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(IsCancelEnabled));
                OnPropertyChanged(nameof(IsLoginEnabled));

            }
        }

        public bool IsPasswordVisible
        {
            get => _isPasswordVisible;
            set
            {
                _isPasswordVisible = value;
                OnPropertyChanged(nameof(IsPasswordVisible));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                if (string.IsNullOrEmpty(_errorMessage))
                    ErrorColor = Colors.Transparent;
                OnPropertyChanged(nameof(ErrorMessage));
                
                
            }
        }

        public Color ErrorColor
        {
            get => _errorColor;
            
            set
            { 
                _errorColor = value;
                OnPropertyChanged(nameof(ErrorColor));
            }
        }
        public bool IsLoginEnabled
        {
            get 
            {
                return !(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password));
            }
        }

        public bool IsCancelEnabled
        {
            get
            {
                return !(string.IsNullOrEmpty(Username) && string.IsNullOrEmpty(Password));
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand CancelCommand { get; }

        public ICommand RegisterCommand { get; }
        public ICommand TogglePasswordVisibilityCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLogin, () => IsLoginEnabled);
            CancelCommand = new Command(OnCancel, () => IsCancelEnabled);
            
            TogglePasswordVisibilityCommand = new Command(TogglePasswordVisibility);
        }

        private void OnLogin()
        {
            if (Username == "admin" && Password == "1234")
            {
                ErrorMessage = "Access is granted";
                ErrorColor = Colors.Green;
                
            }
            else
            {
                ErrorMessage = "Username or Password is not valid";
                ErrorColor = Colors.Red;
            }
            
        }

        private void OnCancel()
        {
            Username = string.Empty;
            Password = string.Empty;
            ErrorMessage = string.Empty;
            ErrorColor = Colors.Transparent;
        }

        

        private void TogglePasswordVisibility()
        {
            IsPasswordVisible = !IsPasswordVisible;
        }

        

        
        }
}