
using PhoneBook.Models;
using PhoneBook.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Contact = PhoneBook.Models.Contact;
namespace PhoneBook.ViewModels
{
    class ContactsPageViewModel : ViewModelBase
    {
        #region Fields
        
        private ObservableCollection<Contact> contacts;
        private ContactService contactService;
        private List<Contact> fullList;
        private bool isRefreshing;



        #region נבחר מהרשימה
        private Contact selectedContact;
        public Contact SelectedContact
        {
            get
            {
                return selectedContact;
            }
            set
            {
                if (selectedContact != value)
                {
                    selectedContact = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region בחירת אוסף פריטים מהרישמה
        public ObservableCollection<object> SelectedContacts
        {
            get; set;
        }

        #endregion


        #endregion

        #region Properties
        public bool IsRefreshing
        {
            get
            {
                return isRefreshing;
            }
            set
            {
                if (isRefreshing != value)
                {
                    isRefreshing = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Contact> Contacts
        {
            get => contacts;
            set
            {
                if (contacts != value)
                {
                    contacts = value;
                    OnPropertyChanged();
                }
            }
        }
       

        #region COMMANDS

        public ICommand DeleteCommand
        {
            get; private set;
        }
        public ICommand RefreshCommand
        {
            get; private set;
        }
        public ICommand FilterAbovePriceCommand
        {
            get; private set;
        }

        public ICommand FilterBelowPriceCommand
        {
            get; private set;
        }

        #region Navigation
        public ICommand ShowDetailsCommand
        {
            get; private set;
        }
        //Shell Navigation Pass Arguments


        //Shell Navigation Pass Object

        #endregion

        #endregion

        #region Constructor
        public ContactsPageViewModel()
        {
            #region Init Data
            
            contactService = new ContactService();
            
            fullList = contactService.GetContacts();
            Contacts = new ObservableCollection<Contact>();
            foreach (var contact in fullList)
                Contacts.Add(contact);
            #endregion

            #region Init Commands
            RefreshCommand = new Command(Refresh);
            DeleteCommand = new Command<Contact>((t) => { if (contactService.DeleteContact(t)) Refresh(); });

            #region Navigation Commands
            //Navigation with Parameters
            // ShowDetailsCommand = new Command(async() => { await GotoWithArguments(); });

            //Navigation With Object
            ShowDetailsCommand = new Command(async () => { await GoToDetailsPage(); });
            #endregion

            #region Commands By LINQ
            //FilterAbovePriceCommand = new Command(() => Toys = new ObservableCollection<Toy>(Toys.Where(t => t.Price > Price)));
            //FilterBelowPriceCommand = new Command(() => {
            //    var toys = Toys.Where(t => t.Price > Price);
            //    foreach (var toy in toys)
            //    {
            //       Toys.Remove(toy);
            //    }
            //});
            #endregion

            #endregion

        }


        #endregion

        #region Methods
        private void Refresh()
        {
            IsRefreshing = true;
            fullList = contactService.GetContacts();
            Contacts = new ObservableCollection<Contact>(fullList);
            
            RefreshCommands();
            IsRefreshing = false;
        }
        

        private void RefreshCommands()
        {
            


        }
        #region Navigation Methods
        //Navigation with parameter
        private async Task GoToDetailsPage()
        {
            if (SelectedContact == null)
                return;
            
            await Shell.Current.GoToAsync($"/Details?id={SelectedContact.Id}");

            SelectedContact = null;


        }



        #endregion
        #endregion

    }
}
#endregion