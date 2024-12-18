using Business.Factories;
using Business.Interfaces;

namespace Presentation.Console_MainApp.Dialogs;

public class MenuDialog (IContactService contactService)
{
    private readonly IContactService _contactService = contactService;
    public void ShowMainMenu()
    {
        var newContact = ContactFactory.Create();
        newContact.FirstName = "Test1";
        newContact.LastName = "Test1";
        newContact.Email = "Test";
        newContact.City = "Test";
        newContact.StreetAddress = "Dasdasd 123";
        newContact.PhoneNumber = "01235456";
        newContact.PostalCode = 12345;

        _contactService.CreateContact(newContact);
    }

}
