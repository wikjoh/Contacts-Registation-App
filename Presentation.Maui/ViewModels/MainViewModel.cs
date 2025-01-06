using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Presentation.Maui.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IContactService _contactService;

    public MainViewModel(IContactService contactService)
    {
        _contactService = contactService;
        UpdateContactList();
    }

    [ObservableProperty]
    private ContactDto _contactForm = new();

    [ObservableProperty]
    private ObservableCollection<ContactModel> _contactList = new();

    [RelayCommand]
    public void AddContactToList()
    {
        if (ContactForm != null && !string.IsNullOrWhiteSpace(ContactForm.FirstName))
        {
            bool result = _contactService.CreateContact(ContactForm);
            if (result)
            {
                UpdateContactList();
                ContactForm = new();
            }
        }
    }



    public void UpdateContactList()
    {
        //ObservableCollection<ContactModel>ContactList = new(_contactService.GetContacts());
        ContactList = new ObservableCollection<ContactModel>(_contactService.GetContacts());
    }
}
