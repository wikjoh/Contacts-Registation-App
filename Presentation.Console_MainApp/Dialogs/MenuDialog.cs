using Business.Factories;
using Business.Helpers;
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
            Console.Clear();
            Console.WriteLine("##### MAIN MENU #####");
            Console.WriteLine("1. List all contacts");
            Console.WriteLine("2. Create new contact");
            Console.WriteLine("3. Delete contact");
            Console.WriteLine();
            Console.Write("Enter option: ");

            var option = Console.ReadLine();
            Console.WriteLine();


            switch (option)
            {
                case "1":
                    ListAllContactsMenu();
                    break;

                case "2":
                    CreateContactMenu();
                    break;

                case "3":
                    DeleteContactMenu();
                    break;

                default:
                    Console.WriteLine("Invalid input. Try again.");
                    break;
            }
        }
    }


    private void ListAllContactsMenu()
    {
        Console.Clear();
        Console.WriteLine("##### LIST ALL CONTACTS #####");
        ListAllContacts();

        Console.WriteLine();
        Console.WriteLine("Press the any key to return.");
        Console.ReadKey();
    }


    private void CreateContactMenu()
    {
        var newContact = ContactFactory.Create();

        Console.Clear();
        Console.WriteLine("##### ADD NEW CONTACT #####");
        newContact.FirstName = PromptInput<string>("Enter first name: ", nameof(newContact.FirstName));
        newContact.LastName = PromptInput<string>("Enter last name: ", nameof(newContact.LastName));
        newContact.Email = PromptInput<string>("Enter email address: ", nameof(newContact.Email));
        newContact.PhoneNumber = PromptInput<string>("Enter phone number: ", nameof(newContact.PhoneNumber));
        newContact.StreetAddress = PromptInput<string>("Enter street address: ", nameof(newContact.StreetAddress));
        newContact.PostalCode = PromptInput<int>("Enter postal code: ", nameof(newContact.PostalCode));
        newContact.City = PromptInput<string>("Enter city: ", nameof(newContact.City));

        _contactService.CreateContact(newContact);
    }


    private void DeleteContactMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("##### DELETE CONTACT #####");
            ListAllContacts();
            Console.WriteLine();

            Console.Write("Enter id of contact to delete, or leave empty to return: ");
            string input = Console.ReadLine() ?? string.Empty;
            if (input == string.Empty) return;

            var parseResult = InputParser.Parse<int>(input);
            if (parseResult.ParseSuccess)
            {
                int idToDelete = parseResult.Parsed;
                if (_contactService.DeleteContact(idToDelete))
                {
                    Console.WriteLine();
                    Console.WriteLine("Contact deleted successfully.");
                    Console.WriteLine("Press enter to return to main menu.");
                    Console.ReadKey();
                    return;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Try again.");
            }
        }
    }

    private void ListAllContacts()
    {
        var contacts = _contactService.GetContacts();
        if (contacts.Count() > 0)
        {
            foreach (var contact in contacts)
            {
                Console.WriteLine($"{contact.Id} {contact.FirstName} {contact.LastName} {contact.Email} {contact.PhoneNumber} {contact.StreetAddress} {contact.PostalCode} {contact.City} {contact.Guid}");
            }
        }
        else Console.WriteLine("Contacts list is empty.");
    }


    private T PromptInput<T>(string prompt, string propertyName)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? String.Empty;
            var parseResult = InputParser.Parse<T>(input);


            if (parseResult.ParseSuccess)
            {
                var parsedInput = parseResult.Parsed;
                var inputValidationResults = InputValidator.Validate<T>(parsedInput!, propertyName);

                if (inputValidationResults == null)
                {
                    return (T)(object)parsedInput!;
                }
                else
                {
                    foreach (var result in inputValidationResults)
                    {
                        Console.WriteLine(result.ErrorMessage);
                    }
                    Console.WriteLine("Please try again.");
                }
            }
            else if (!parseResult.ParseSuccess && typeof(T) == typeof(int))
            {
                Console.WriteLine("Invalid input. Must be a number.");
            }
        }
    }

}
