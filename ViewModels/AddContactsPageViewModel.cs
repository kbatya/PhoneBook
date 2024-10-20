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
using Contact = PhoneBook.Models.Contact;
using PhoneBook.Services;
namespace PhoneBook.ViewModels
{
    class AddContactsPageViewModel : ViewModelBase
    {
        #region list of Contacts and Contacts Type will be moved to Service Folder
        List<Contact> contacts = new();//list of contacts
        private ContactService contactService;
        public List<ContactTypes> ContactTypes
        { get
            {
                return contactService.GetContactTypes();
            }
        }

        #endregion

        #region Fields
        Contact newContact;//adding a contact

        private string name;//contact name    
        private string phoneNumber;//phone number
        private string errorMessage;//error message for name
        private string errorMessagePhone; ////error message for phone
        private ContactTypes selectedType;//selected contact type

        #endregion

        #region Properties

        //contact name
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
                    HandleError();//name validation
                }

            }
        }

        //contact name
        public string PhoneNumber
        {
            get
            {
                return phoneNumber  ;
            }
            set
            {
                if (phoneNumber != value)
                {

                    phoneNumber = value;
                    OnPropertyChanged(nameof(PhoneNumber));
                    HandleErrorPhone();//phone validation

                }

            }
        }
        //contact name validation
        public bool HasError
        {
            get
            {
                return string.IsNullOrEmpty(Name) || !ValidName() ; 
            }

        }

        public bool HasErrorPhone
        {
            get
            {
                return string.IsNullOrEmpty(PhoneNumber) || !ValidPhone();
            }

        }
        //error message
        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                if (errorMessage != value)
                {
                    errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }

        public string ErrorMessagePhone
        {
            get
            {
                return errorMessagePhone;
            }
            set
            {
                if (errorMessagePhone != value)
                {
                    errorMessagePhone = value;
                    OnPropertyChanged(nameof(ErrorMessagePhone));
                }
            }
        }
        //selected contact type
        public ContactTypes SelectedType
        {
            get
            {
                return selectedType;
            }
            set
            {
                if (selectedType != value)
                {
                    selectedType = value;
                    OnPropertyChanged(nameof(SelectedType));

                }
            }
        }
            
        //checking possibility of adding a contact
        public bool CanAddContact
        {
            get
            {
                return !(HasError || HasErrorPhone);
            }
        }

        #endregion

        #region Commands
        //PROPERTY to be Bound to the Button Command
        public ICommand AddContactCommand
        {
            get; private set;
        }
        #endregion

        #region Constructor
        public AddContactsPageViewModel()
        {
            //connect the property with the action method to perform
            //Command([method Name]) --> for void methods
            //Command<type>([method name]--> for methods that has parameters
            contactService = new ContactService();
            AddContactCommand = new Command<string>(AddnewContact);

        }

        #endregion

        #region commands 
        //error logics
        private void HandleError()
        {
            ErrorMessage = string.Empty;
            if (!ValidName())
            {
                ErrorMessage = "The name is too short or consists invalid characters";
            }
            
            OnPropertyChanged(nameof(HasError));
            OnPropertyChanged(nameof(CanAddContact));
        }

        private void HandleErrorPhone()
        {
            ErrorMessagePhone = string.Empty;
            
            if (!ValidPhone())
            {
                ErrorMessagePhone= "The phone number is illegal";
            }
            OnPropertyChanged(nameof(HasErrorPhone));
            OnPropertyChanged(nameof(CanAddContact));
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
        private bool ValidPhone()
        {
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                return false; 
            }
            Regex reg = new Regex(@"^(?:(?:(\+972|972|0)(?:-)?([23489])(?:-)?([0-9]{3})(?:-)?([0-9]{4}))|(?:(\+972|972|0)(?:-)?([5])(?:-)?([0-9]{8})))$");
            return reg.IsMatch(PhoneNumber);
        }
        #endregion

        //Adding a new contact
        private void AddnewContact(string name)
        {
            
            newContact = new Contact() { Id = 1, Name = name,PhoneNumber = PhoneNumber,  Type = SelectedType };
            
            contacts.Add(newContact);



        }
        #endregion
    }
}
