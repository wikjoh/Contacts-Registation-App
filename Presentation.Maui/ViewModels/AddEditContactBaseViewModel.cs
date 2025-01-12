using Business.Dtos;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Presentation.Maui.ViewModels;

// Shared base viewmodel with validation logic used in both Add- and Edit viewmodels
public abstract partial class AddEditContactBaseViewModel : ObservableObject
{
    [ObservableProperty]
    public string? firstNameError = null!;
    partial void OnFirstNameErrorChanged(string? value)
    {
        IsFirstNameErrorVisible = !string.IsNullOrEmpty(value);
    }

    [ObservableProperty]
    public bool isFirstNameErrorVisible = true;


    [ObservableProperty]
    public string? lastNameError = null!;
    partial void OnLastNameErrorChanged(string? value)
    {
        IsLastNameErrorVisible = !string.IsNullOrEmpty(value);
    }

    [ObservableProperty]
    public bool isLastNameErrorVisible = true;


    [ObservableProperty]
    public string? emailError = null!;
    partial void OnEmailErrorChanged(string? value)
    {
        IsEmailErrorVisible = !string.IsNullOrEmpty(value);
    }

    [ObservableProperty]
    public bool isEmailErrorVisible = true;


    [ObservableProperty]
    public string? phoneNumberError = null!;
    partial void OnPhoneNumberErrorChanged(string? value)
    {
        IsPhoneNumberErrorVisible = !string.IsNullOrEmpty(value);
    }

    [ObservableProperty]
    public bool isPhoneNumberErrorVisible = true;


    [ObservableProperty]
    public string? streetAddressError = null!;
    partial void OnStreetAddressErrorChanged(string? value)
    {
        IsStreetAddressErrorVisible = !string.IsNullOrEmpty(value);
    }

    [ObservableProperty]
    public bool isStreetAddressErrorVisible = true;


    [ObservableProperty]
    public string? postalCodeError = null!;
    partial void OnPostalCodeErrorChanged(string? value)
    {
        IsPostalCodeErrorVisible = !string.IsNullOrEmpty(value);
    }

    [ObservableProperty]
    public bool isPostalCodeErrorVisible = true;


    [ObservableProperty]
    public string? cityError = null!;
    partial void OnCityErrorChanged(string? value)
    {
        IsCityErrorVisible = !string.IsNullOrEmpty(value);
    }

    [ObservableProperty]
    public bool isCityErrorVisible = true;


    protected virtual ContactModel GetContactEdit()
    {
        return null!;
    }

    protected virtual ContactDto GetContactForm()
    {
        return null!;
    }


    public void ValidateProperty(string propertyName)
    {
        object objectToValidate = null!;

        var instanceChild = this.GetType();
        if (instanceChild == typeof(EditContactViewModel))
        {
            objectToValidate = GetContactEdit();
        }
        else if (instanceChild == typeof(AddContactViewModel))
        {
            objectToValidate = GetContactForm();
        }


        // Compare both Dto And Model to Dto data annotations since requirements are the same
        var context = new ValidationContext(new ContactDto()) { MemberName = propertyName }; 
        var results = new List<ValidationResult>();
        bool isValid = Validator.TryValidateProperty(
            objectToValidate!.GetType().GetProperty(propertyName)!.GetValue(objectToValidate),
            context,
            results);


        switch (propertyName)
        {
            case "FirstName":
                FirstNameError = isValid ? string.Empty : results[0].ErrorMessage!;
                break;
            case "LastName":
                LastNameError = isValid ? string.Empty : results[0].ErrorMessage!;
                break;
            case "Email":
                EmailError = isValid ? string.Empty : results[0].ErrorMessage!;
                break;
            case "PhoneNumber":
                PhoneNumberError = isValid ? string.Empty : results[0].ErrorMessage!;
                break;
            case "StreetAddress":
                StreetAddressError = isValid ? string.Empty : results[0].ErrorMessage!;
                break;
            case "PostalCode":
                PostalCodeError = isValid ? string.Empty : results[0].ErrorMessage!;
                break;
            case "City":
                CityError = isValid ? string.Empty : results[0].ErrorMessage!;
                break;
            default:
                break;
        }
    }
}
