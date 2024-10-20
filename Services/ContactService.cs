using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBook.Models;
using Contact = PhoneBook.Models.Contact;
using System.Text.Json;
namespace PhoneBook.Services
{
    //DAL in future
    public class ContactService : IContactService
    {
        int id;
        List<Contact>? contacts;
        List<ContactTypes>? contactTypes;
        User user;
        public ContactService()
        {
            InitContactTypes();
            InitContacts();
            InitUser();
            id = 0;
        }

        private void InitContactTypes()
        {
            contactTypes = new List<ContactTypes>()
        {
            new ContactTypes()
            {
                Id = 1, Name = "enemy"
            },

            new ContactTypes()
            {
            Id = 2, Name = "family"
            },
            new ContactTypes()
            {
            Id = 3, Name = "friend"
            },
            new ContactTypes()
            {
            Id = 4, Name = "stranger"
            }
        };
        }

        private void InitUser()
        {
            user = new User() { Name = "Batya Bukhel", Username = "batya.bukhel@gmail.com", Password = "diMa1972$" };                                          
        }

        private void InitContacts()
        {
            contacts = new List<Contact>()
            {
                new Contact()
                {
                    Id=++id,


                    Name="Keir Starmer",
                    Image="starmer.jpg",
                    PhoneNumber="0523545352",
                    CountryFlag="united_kingdom.png",
                    Type=contactTypes[2]
                },
                new Contact()
                {
                    Id=++id,


                    Name="Vladimir Putin",
                    Image="putin.jpg",
                    PhoneNumber="0587378749",
                    CountryFlag="russia.png",
                    Type=contactTypes[3]
                },
                new Contact()
                {
                    Id=++id,


                    Name="Donald Trump",
                    Image="trump.jpg",
                    PhoneNumber="0587987719",
                    CountryFlag="united_states.png",
                    Type=contactTypes[2]
                },
                new Contact()
                {
                    Id=++id,

                    Name="Emmanuel Macron",
                    Image="macron.jpg",
                    PhoneNumber="0587382619",
                    CountryFlag="france.png",
                    Type=contactTypes[3]
                },
                new Contact()
                {
                    Id=++id,

                    Name="Yoon Suk Yeol",
                    Image="yeol.jpg",
                    PhoneNumber="0587886119",
                    CountryFlag="south_kore.png",
                    Type=contactTypes[3]
                },
                
                new Contact()
                {
                    Id=++id,

                    Name="Ali Hosseini Khamenei",
                    Image="khameini.jpg",
                    PhoneNumber="0581111119",
                    CountryFlag="iran.png",
                    Type=contactTypes[0]
                }
            };
        }

        public User getUser
        {
            get
            {
                return user;
            }
        }
        public List<Contact>? GetContacts()
        {
            return contacts?.ToList();
        }

        public List<ContactTypes>? GetContactTypes()
        {
            return contactTypes?.ToList();
        }

        public List<Contact> GetContactByType(ContactTypes type)
        {
            #region By LINQ
            //   return  Contacts.Where(t=>t.Type.Id==type.Id).ToList();
            #endregion
            List<Contact> result = new();
            foreach (Contact t in contacts)
            {
                if (t.Type is not null && t.Type.Id == type.Id)
                    result.Add(t);
            }
            return result;
        }



        public bool AddContact(Contact contact)
        {
            if (contacts != null)
            {
                contacts.Add(contact);
                return true;
            }
            return false;

        }

        public bool DeleteContact(Contact contact)
        {
            foreach (Contact t in contacts)
            {
                if (t.Id == contact.Id)
                    return contacts.Remove(t);
            }
            return false;
            
        }

        public User Login(string username, string password)
        {
            string savedJson = Preferences.Default.Get("userObj", string.Empty);
            User user = JsonSerializer.Deserialize<User>(savedJson);
            if (user is not null && user.Username == username && user.Password == password) 
                return user;
            return null;    
        }

        public bool Register(User user)
        {
            if (user is not null)
            {
                Preferences.Default.Set("userObj", JsonSerializer.Serialize(user));
                return true;
            }
            return false;

        }

        public bool UpdateUser(User user)
        {
            if (user is not null && this.user is not null && this.user.Username == user.Username)
            {
                //update user in preferences
                this.user = user;
                return true;
            }
            return false;
        }
    }

}
