using Microsoft.Maui.Controls.PlatformConfiguration;
using PhoneBook.Models;
using PhoneBook.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Contact = PhoneBook.Models.Contact;

namespace PhoneBook.ViewModels
{
    
        #region פרמטרים ממסכים אחרים
        //פרמטר ראשון - השם של התכונה במסך החדש
        //פרמטר שני- שם של המפתח במילון או השם של הפרמטר במחרוזת 

        [QueryProperty(nameof(SelectedContact), "Contact")]
        [QueryProperty(nameof(Id), "id")]
        #endregion
        public class ContactDetailsPageViewModel : ViewModelBase
        {
            private IContactService contactService;
            private int id;
            private Contact? selectedContact;

            /*add support for changing photo
             *add Property that updates and reads the SelectedContact.Image
             *
             *
             */
            public ICommand ChangePhotoCommand
            {
                get; private set;
            }

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

                        
                    }


                }
            }
            /*
             * add Command for Uploading Image
             * 
             */

            

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



            
            

            
        }
    }

