using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using PhoneBook.Helpers;
using PhoneBook.Models;

using PhoneBook.Services;
using Microsoft.Maui.Controls;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace PhoneBook.ViewModels
{
    class UserUpdatePageViewModel : ViewModelBase
    {
        #region list of Contacts and Contacts Type will be moved to Service Folder
        
        private IContactService contactService;


        #endregion

        #region Fields
        User newUser;//updating a user

        private string name;//user name    
        private string username;
        private string password;
        private string errorMessageName;//error message for name
        private string errorMessageUsername;
        private string errorMessagePassword;

        #endregion

        #region Properties

        //user name
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {

                    name = value;
                    OnPropertyChanged(nameof(Name));
                    HandleErrorName();//name validation
                }

            }
        }

       
        //user name validation
        public bool HasErrorName
        {
            get
            {
                return !ValidName();
            }

        }

        
        //error message
        public string ErrorMessageName
        {
            get
            {
                return errorMessageName;
            }
            set
            {
                if (errorMessageName != value)
                {
                    errorMessageName = value;
                    OnPropertyChanged(nameof(ErrorMessageName));
                }
            }
        }

        //user username
        public string Username
        {
            get
            {
                return username;
            }
            
        }


        


        

        //password
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (password != value)
                {

                    password = value;
                    OnPropertyChanged(nameof(Password));
                    HandleErrorPassword();//name validation
                }

            }
        }


        //user name validation
        public bool HasErrorPassword
        {
            get
            {
                return !ValidPassword();
            }

        }


        //error message
        public string ErrorMessagePassword
        {
            get
            {
                return errorMessagePassword;
            }
            set
            {
                if (errorMessagePassword != value)
                {
                    errorMessageName = value;
                    OnPropertyChanged(nameof(ErrorMessagePassword));
                }
            }
        }
        //checking possibility of adding a user
        public bool CanUpdateUser
        {
            get
            {
                return !(HasErrorName ||  HasErrorPassword);
            }
        }

        #endregion

        #region Commands
        //PROPERTY to be Bound to the Button Command
        public ICommand UpdateUserCommand
        {
            get; private set;
        }
       
        #endregion

        #region Constructor
        public UserUpdatePageViewModel()
        {
            //connect the property with the action method to perform
            //Command([method Name]) --> for void methods
            //Command<type>([method name]--> for methods that has parameters
            contactService = new ContactService();
            User user = contactService.getUser;
            username = user.Username;
            Name = user.Name;
            Password = user.Password;
            UpdateUserCommand = new Command<string>(UpdateUser);

        }

        #endregion

        #region commands 
        //error logics
        private void HandleErrorName()
        {
            ErrorMessageName = string.Empty;
            if (!ValidName())
            {
                ErrorMessageName = "The name is too short or consists invalid characters";
            }

            OnPropertyChanged(nameof(HasErrorName));
            OnPropertyChanged(nameof(CanUpdateUser));
        }


        
        private void HandleErrorPassword()
        {
            ErrorMessagePassword = string.Empty;
            if (!ValidPassword())
            {
                ErrorMessagePassword = "The name is too short or consists invalid characters";
            }

            OnPropertyChanged(nameof(HasErrorPassword));
            OnPropertyChanged(nameof(CanUpdateUser));
        }
        #region Implement Valid Checks
        //name Should start Capital letter, at lease 4 letter word
        //can consist only English letters, space, hyphen or apostrophe
        //Must start with a capital letter
        //At least 4 letters


        private bool ValidName()
        {
            if (string.IsNullOrEmpty(Name))
                return false;
            Regex reg = new Regex(@"^[A-Z][a-zA-Z\-\s']{3,}$");
            return reg.IsMatch(Name);
        }

        
        /* pattern for legal Israeli phone numbers
         Landlines(area codes 2, 3, 4, 8, 9) :
        02-1234567, 03-1234567 ,+97221234567 ,97231234567
        Mobile numbers(area code 5):
        050-1234567, 0501234567, +972501234567, 972501234567
        It allows for optional hyphens and can handle numbers with or without
        the country code(+972 or 972).*/
        
        private bool ValidPassword()
        {
            if (string.IsNullOrEmpty(Password))
                return false;
            // check validation of username as legal email
            Regex reg = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':""\|,.<>\/?])[A-Za-z\d!@#$%^&*()_+\-=\[\]{};':""\|,.<>\/? ]{8,}$");
            return reg.IsMatch(Password);
        }
        //strong password that requires:

        //At least 8 characters
        //At least one uppercase letter
        //At least one lowercase letter
        //At least one number
        //At least one special character
        #endregion

        //Adding a new contact
        private void UpdateUser(string name)
        {

            newUser = new User() { Name = name, Username = username, Password = password };

            contactService.UpdateUser(newUser);


        }
        #endregion
    }
}
