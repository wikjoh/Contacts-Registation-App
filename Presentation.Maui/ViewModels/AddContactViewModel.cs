using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Presentation.Maui.ViewModels;

public partial class AddContactViewModel : AddEditContactBaseViewModel
{
    private readonly IContactService _contactService;

    public AddContactViewModel(IContactService contactService)
    {
        _contactService = contactService;
    }

    [ObservableProperty]
    private ContactDto contactForm = new();


    [RelayCommand]
    public async Task AddContact()
    {
        if (ContactForm != null
            && !IsFirstNameErrorVisible
            && !IsLastNameErrorVisible
            && !IsEmailErrorVisible
            && !IsPhoneNumberErrorVisible
            && !IsStreetAddressErrorVisible
            && !IsPostalCodeErrorVisible
            && !IsCityErrorVisible)
        {
            bool addSuccess = _contactService.CreateContact(ContactForm);
            if (addSuccess)
            {
                ContactForm = new();
            }

            await Shell.Current.GoToAsync("//ListContactsView");
        }
        else
        {
            await Application.Current!.Windows[0].Page!.DisplayAlert("Error", "Form is empty or contains errors.", "OK");
        }
    }


    protected override ContactDto GetContactForm() => ContactForm;
}
