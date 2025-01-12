using Presentation.Maui.ViewModels;
using System.Text.RegularExpressions;

namespace Presentation.Maui.Views;

public partial class EditContactView : ContentPage
{
	public EditContactView(EditContactViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

	private void OnFirstNameChanged(object sender, TextChangedEventArgs e)
	{
		(BindingContext as EditContactViewModel)?.ValidateProperty("FirstName");
	}

    private void OnLastNameChanged(object sender, TextChangedEventArgs e)
    {
        (BindingContext as EditContactViewModel)?.ValidateProperty("LastName");
    }

    private void OnEmailChanged(object sender, TextChangedEventArgs e)
    {
        (BindingContext as EditContactViewModel)?.ValidateProperty("Email");
    }

    private void OnPhoneNumberChanged(object sender, TextChangedEventArgs e)
    {
        (BindingContext as EditContactViewModel)?.ValidateProperty("PhoneNumber");

        // If the text field is empty or null then leave.
        string input = e.NewTextValue;
        if (String.IsNullOrEmpty(input))
            return;

        // If the text field only contains numbers then leave.
        if (!Regex.Match(input, "^[0-9]+$").Success)
        {
            // This returns to the previous valid state.
            var entry = sender as Entry;
            entry!.Text = (string.IsNullOrEmpty(e.OldTextValue)) ?
                    string.Empty : e.OldTextValue;
        }
    }

    private void OnStreetAddressChanged(object sender, TextChangedEventArgs e)
    {
        (BindingContext as EditContactViewModel)?.ValidateProperty("StreetAddress");
    }

    private void OnPostalCodeChanged(object sender, TextChangedEventArgs e)
    {
        (BindingContext as EditContactViewModel)?.ValidateProperty("PostalCode");

        // If the text field is empty or null then leave.
        string input = e.NewTextValue;
        if (String.IsNullOrEmpty(input))
            return;

        // If the text field only contains numbers then leave.
        if (!Regex.Match(input, "^[0-9]+$").Success)
        {
            // This returns to the previous valid state.
            var entry = sender as Entry;
            entry!.Text = (string.IsNullOrEmpty(e.OldTextValue)) ?
                    string.Empty : e.OldTextValue;
        }
    }

    private void OnCityChanged(object sender, TextChangedEventArgs e)
    {
        (BindingContext as EditContactViewModel)?.ValidateProperty("City");
    }
}