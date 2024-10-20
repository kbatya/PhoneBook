using Microsoft.Maui.Controls.PlatformConfiguration;
using PhoneBook.Models;
using PhoneBook.Services;
using PhoneBook.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PhoneBook.Views;
namespace PhoneBook.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        #region Fields
        private IContactService service;//service
        #endregion

        #region Properties

        
        #endregion

        #region Commands
        public ICommand LoginCommand { get; set; }
        #endregion
        public LoginPageViewModel(IContactService service)
        {
            this.service = service;
            InServerCall = false;
            RegisterCommand = new Command(async () => await GoToRegistrationPage());
            this.LoginCommand = new Command(OnLogin);

        }

        private void OnLogin()
        {
            //Choose the way you want to block the page while indicating a server call
            InServerCall = true;


            User loginUser = this.service.Login(Email, Password);

            InServerCall = false;

            //Set the application logged in user to be whatever user returned (null or real user)

            if (loginUser is null)
            {

                Application.Current.MainPage.DisplayAlert("Login", "Login Failed!", "ok");
            }
            else
            {
                App.user = loginUser;
                Application.Current.MainPage.DisplayAlert("Login", $"Login Succeed! for {service.getUser.Name}", "ok");
                App.Current.MainPage = new AppShell();
                Shell.Current.GoToAsync("///Contacts");
            }
        }



        private bool inServerCall;
        public bool InServerCall
        {
            get
            {
                return this.inServerCall;
            }
            set
            {
                this.inServerCall = value;
                OnPropertyChanged("NotInServerCall");
                OnPropertyChanged("InServerCall");
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
                OnPropertyChanged();
            }
        }

        private string email;
        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = value;
                OnPropertyChanged();
            }
        }
        public bool NotInServerCall
        {
            get
            {
                return !this.InServerCall;
            }
        }
        public ICommand RegisterCommand
        {
            get; private set;
        }
        private async Task GoToRegistrationPage()
        {
            try
            {
                if (Shell.Current == null)
                    App.Current.MainPage = new RegistrationPage();
                     
                else
                    Shell.Current.GoToAsync("//Registration");
                
                
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error navigating to Registration page: {ex}");

                // Inform the user
                await Application.Current.MainPage.DisplayAlert("Error", "Unable to navigate to the Registration page. Please try again.", "OK");
            }
        }
    }
}