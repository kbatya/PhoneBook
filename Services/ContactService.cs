using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBook.Models;
using Contact = PhoneBook.Models.Contact;

namespace PhoneBook.Services
{
    //DAL in future
    class ContactService : IContactService
    {
        int id;
        List<Contact>? contacts;
        List<ContactTypes>? contactTypes;
        public ContactService()
        {
            InitContactTypes();
            InitContacts();
            id = 0;
        }

        private void InitContactTypes()
        {
            contactTypes = new List<ContactTypes>()
        {
            new ContactTypes()
            {
                Id = 1, Name = "work"
            },

            new ContactTypes()
            {
            Id = 2, Name = "family"
            },
            new ContactTypes()
            {
            Id = 3, Name = "cellular"
            }
        };
        }

        private void InitContacts()
        {
            contacts = new List<Contact>()
            {
                new Contact()
                {
                    Id=++id,


                    Name="Dima",
                    PhoneNumber="0528505152",
                    Type=contactTypes[2]
                },
                new Contact()
                {
                    Id=++id,


                    Name="Tamara",
                    PhoneNumber="0586278749",
                    Type=contactTypes[1]
                },
                new Contact()
                {
                    Id=++id,


                    Name="Rosa",
                    PhoneNumber="0587987719",
                    Type=contactTypes[0]
                },
                new Contact()
                {
                    Id=++id,

                    Name="Caleb",
                    PhoneNumber="0587382619",
                    Type=contactTypes[1]
                }

            };
        }

        public List<Contact>? GetContacts()
        {
            return contacts?.ToList();
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
            #region using LINQ
            // return Contacts.Remove(Contacts.Find((x)=>x.Id==Contact.Id));
            #endregion
        }
    }

}
