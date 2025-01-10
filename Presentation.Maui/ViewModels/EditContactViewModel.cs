using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Presentation.Maui.ViewModels;

public partial class EditContactViewModel : ObservableObject, IQueryAttributable
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
        _contactService.UpdateContact(ContactEdit);
        await Shell.Current.GoToAsync("//ListContactsView");
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        ContactEdit = (query["Contact"] as ContactModel)!;
    }
}
