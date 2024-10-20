using Microsoft.Maui.Controls.PlatformConfiguration;
using PhoneBook.Models;
using PhoneBook.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact = PhoneBook.Models.Contact;

namespace PhoneBook.ViewModels
{
    
        #region 
        

        [QueryProperty(nameof(SelectedContact), "Contact")]
        [QueryProperty(nameof(Id), "id")]
        #endregion
        class ContactDetailsPageViewModel : ViewModelBase
        {
            private ContactService contactService;
            private int id;
            private Contact? selectedContact;


            public int Id
            {
                get
                {
                    return id;
                }
                set
                {
                    if (id != value)
                    {
                        id = value;
                        OnPropertyChanged();

                        //Fetch the contact by Id
                        FetchToyById();
                    }


                }
            }

            private void FetchToyById()
            {
            SelectedContact = contactService?.GetContacts()?.Where(t => t.Id == id).FirstOrDefault();
            }

            public Contact? SelectedContact
            {
                get => selectedContact;
                set
                {
                    if (selectedContact != value)
                    {
                    selectedContact = value;
                        OnPropertyChanged();
                        
                    }
                }
            }



            public ContactDetailsPageViewModel()
            {
                contactService = new ContactService();
           


            }


        }
    }