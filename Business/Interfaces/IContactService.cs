using Business.Dtos;
using Business.Models;

namespace Business.Interfaces;

public interface IContactService
{
    bool CreateContact(ContactDto contactForm);
    IEnumerable<ContactModel> GetContacts();
    bool DeleteContact(int contactId);
}
