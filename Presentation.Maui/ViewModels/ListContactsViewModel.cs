using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Presentation.Maui.ViewModels;

public partial class ListContactsViewModel : ObservableObject
{
    private readonly IContactService _contactService;

    public ListContactsViewModel(IContactService contactService)
    {
        _contactService = contactService;
        UpdateContactList();

        _contactService.ContactsUpdated += (sender, e) =>
        {
            UpdateContactList();
        };
    }

    [ObservableProperty]
    private ObservableCollection<ContactModel> _contactList = new();


    [RelayCommand]
    private async Task NavigateToEdit(ContactModel contact)
    {
        var parameters = new ShellNavigationQueryParameters
        {
            { "Contact", contact }
        };

        await Shell.Current.GoToAsync("EditContactView", parameters);
    }



    [RelayCommand]
    public void DeleteContact(ContactModel contact)
    {
        bool deleteSuccess = _contactService.DeleteContact(contact.Id);
        if (deleteSuccess)
        {
            UpdateContactList();
        }
        else
        {
            Debug.WriteLine("Failed to delete contact.");
        }
    }

    public void UpdateContactList()
    {
        ContactList = new ObservableCollection<ContactModel>(_contactService.GetContacts());
    }
}