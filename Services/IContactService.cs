using PhoneBook.Models;

namespace PhoneBook.Services
{
    public interface IContactService
    {
        bool AddContact(Models.Contact contact);
        bool DeleteContact(Models.Contact contact);
        List<Models.Contact> GetContactByType(ContactTypes type);
        List<Models.Contact>? GetContacts();
    }
}