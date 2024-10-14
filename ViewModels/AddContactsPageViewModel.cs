using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using PhoneBook.Helpers;
using PhoneBook.Models;
using Contact = PhoneBook.Models.Contact;
namespace PhoneBook.ViewModels
{
    class AddContactsPageViewModel : ViewModelBase
    {
        #region list of Contacts and Contacts Type will be moved to Service Folder
        List<Contact> Contacts = new();//רשימת הצעצועים
        public List<ContactTypes> ContactTypes
        { get; set; } = new List<ContactTypes>()
        {
            new ContactTypes()
            {
                Id = 1, Name = "cellular"
            },

            new ContactTypes()
            {
            Id = 2, Name = "work"
            },
            new ContactTypes()
            {
            Id = 3, Name = "family"
            }
        };

        #endregion

        #region Fields
        Contact newContact;//adding a contact

        private string name;//contact name    
        private string phoneNumber;//phone number
        private string errorMessage;//error message
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
                   
                }

            }
        }
        //contact name validation
        public bool HasError
        {
            get
            {
                return string.IsNullOrEmpty(Name) || !ValidName();
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
                return !HasError  && SelectedType != null;
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

            AddContactCommand = new Command<string>(AddnewContact);

        }

        #endregion

        #region commands 
        //error logics
        private void HandleError()
        {
            if (!ValidName())
            {
                ErrorMessage = "The name is too short or consists invalid characters";
            }
            else
                ErrorMessage = string.Empty;
            OnPropertyChanged(nameof(HasError));
            OnPropertyChanged(nameof(CanAddContact));
        }

        #region Implement Valid Checks
        //name Should start Capital letter, at lease 4 letter word
        //can consist only English letters, space, hyphen or apostrophe
        //Must start with a capital letter
        //At least 4 letters


        private bool ValidName()
        {
            Regex reg = new Regex(@"^[A - Z][a - zA - Z']{2,}(?:[-\s][A-Z]?[a-zA-Z'] *) * $");
            return reg.IsMatch(Name);
        }
        #endregion

        //Adding a new contact
        private void AddnewContact(string name)
        {
            string phoneNumber = "0586278749";
            newContact = new Contact() { Id = 1, Name = name,PhoneNumber = phoneNumber,  Type = SelectedType };
            
            Contacts.Add(newContact);



        }
        #endregion
    }
}
