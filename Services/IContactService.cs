
using PhoneBook.Models;

namespace PhoneBook.Services
{
    public interface IContactService
    {
        public User getUser { get; }

        public bool AddContact(Models.Contact contact);
        public bool DeleteContact(Models.Contact contact);
        public List<Models.Contact> GetContactByType(ContactTypes type);
        public List<Models.Contact>? GetContacts();
        public List<ContactTypes>? GetContactTypes();
        public User Login(string username, string password);
        public bool Register(User user);
        public bool UpdateUser(User user);
    }
}