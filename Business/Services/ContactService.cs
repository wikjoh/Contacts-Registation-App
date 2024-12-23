using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using System.Diagnostics;

namespace Business.Services;

public class ContactService(IContactRepository contactRepository) : IContactService
{
    private readonly IContactRepository _contactRepository = contactRepository;
    private List<ContactModel> _contacts = contactRepository.ReadFromFile() ?? [];


    public bool CreateContact(ContactDto contactForm)
    {
        try
        {
            var contactModel = ContactFactory.Create(contactForm);

            if (contactModel != null)
            {
                contactModel.Id = _contacts.Count() != 0 ? _contacts.Last().Id + 1 : 1;

                _contacts.Add(contactModel);
                _contactRepository.SaveToFile(_contacts);
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to create contact. {ex.Message}");
            return false;
        }
    }

    public IEnumerable<ContactModel> GetContacts()
    {
        _contacts = _contactRepository.ReadFromFile() ?? [];
        return _contacts;
    }
}
