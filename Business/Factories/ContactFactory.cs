using Business.Dtos;
using Business.Models;
using System.Diagnostics;

namespace Business.Factories;

public static class ContactFactory
{
    public static ContactDto Create()
    {
        return new ContactDto();
    }

    public static ContactModel? Create(ContactDto contactForm)
    {
        try
        {
            return new ContactModel
            {
                FirstName = contactForm.FirstName,
                LastName = contactForm.LastName,
                Email = contactForm.Email,
                PhoneNumber = contactForm.PhoneNumber,
                StreetAddress = contactForm.StreetAddress,
                PostalCode = contactForm.PostalCode,
                City = contactForm.City
            };
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating ContactModel. {ex.Message}");
            return null;
        }
    }

}
