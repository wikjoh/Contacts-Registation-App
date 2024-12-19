using Business.Factories;
using Business.Interfaces;

namespace Presentation.Console_MainApp.Dialogs;

public class MenuDialog (IContactService contactService)
{
    private readonly IContactService _contactService = contactService;
    bool keepRunning = true;

    public void ShowMainMenu()
    {
        while (keepRunning)
        {
            Console.WriteLine("##### MAIN MENU #####");
            Console.WriteLine("1. List all contacts");
            Console.WriteLine("2. Create new contact");
            Console.WriteLine();
            Console.Write("Enter option: ");

            var option = Console.ReadLine();
            Console.WriteLine();


            switch (option)
            {
                case "1":
                    ListAllContacts();
                    break;

                case "2":
                    CreateContact();
                    break;

                default:
                    Console.WriteLine("Invalid input. Try again.");
                    break;
            }
        }
    }


    private void ListAllContacts()
    {
        var contacts = _contactService.GetContacts();
        foreach (var contact in contacts)
        {
            Console.WriteLine($"{contact.Id} {contact.FirstName} {contact.LastName} {contact.Email} {contact.PhoneNumber} {contact.StreetAddress} {contact.PostalCode} {contact.City} {contact.Guid}");
        }
    }


    private void CreateContact()
    {
        var newContact = ContactFactory.Create();

        Console.WriteLine("##### ADD NEW CONTACT #####");
        newContact.FirstName = PromptInput<string>("Enter first name: ");
        newContact.LastName = PromptInput<string>("Enter last name: ");
        newContact.Email = PromptInput<string>("Enter email address: ");
        newContact.PhoneNumber = PromptInput<string>("Enter phone number: ");
        newContact.StreetAddress = PromptInput<string>("Enter street address: ");
        newContact.PostalCode = PromptInput<int>("Enter postal code: ");
        newContact.City = PromptInput<string>("Enter city: ");

        _contactService.CreateContact(newContact);
    }


    private T PromptInput<T>(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine()!;

            if (typeof(T) == typeof(int))
            {
                if (int.TryParse(input, out int intInput))
                {
                    return (T)(object)intInput;
                }
                else
                {
                    Console.WriteLine("Must be a number. Try again.");
                }
            }
            else
            {
                return (T)(object)input;
            }
        }
    }
}
