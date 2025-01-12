using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace Presentation.Maui.ViewModels;

public partial class EditContactViewModel : AddEditContactBaseViewModel, IQueryAttributable
{
    private readonly IContactService _contactService;

    public EditContactViewModel(IContactService contactService)
    {
        _contactService = contactService;
    }


    [ObservableProperty]
    private ContactModel contactEdit = new();

    [RelayCommand]
    public async Task UpdateContact()
    {
        if (!IsFirstNameErrorVisible
            &&  !IsLastNameErrorVisible
            && !IsEmailErrorVisible
            && !IsPhoneNumberErrorVisible
            && !IsStreetAddressErrorVisible
            && !IsPostalCodeErrorVisible
            && !IsCityErrorVisible)
        {
            _contactService.UpdateContact(ContactEdit);
            await Shell.Current.GoToAsync("//ListContactsView");
        }
        else
        {
            await Application.Current!.Windows[0].Page!.DisplayAlert("Error", "Form is empty or contains errors.", "OK");
        }
    }


    protected override ContactModel GetContactEdit() => ContactEdit;


    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        ContactEdit = (query["Contact"] as ContactModel)!;
    }
}
