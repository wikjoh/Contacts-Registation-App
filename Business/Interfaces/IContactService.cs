using Business.Dtos;
using Business.Models;

namespace Business.Interfaces;

public interface IContactService
{
    event EventHandler? ContactsUpdated;
    IEnumerable<ContactModel> GetContacts();
    bool CreateContact(ContactDto contactForm);
    bool DeleteContact(int contactId);
    bool UpdateContact(ContactModel contact);
}
