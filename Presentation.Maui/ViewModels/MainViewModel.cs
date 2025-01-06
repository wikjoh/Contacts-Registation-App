using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

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
    public void AddContact()
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

    [RelayCommand]
    public void DeleteContact(ContactModel contact)
    {
        if (_contactService.DeleteContact(contact.Id))
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
        //ObservableCollection<ContactModel>ContactList = new(_contactService.GetContacts());
        ContactList = new ObservableCollection<ContactModel>(_contactService.GetContacts());
    }
}
