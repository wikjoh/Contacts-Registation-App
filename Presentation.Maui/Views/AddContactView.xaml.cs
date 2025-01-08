using Presentation.Maui.ViewModels;

namespace Presentation.Maui.Views;

public partial class AddContactView : ContentPage
{
	public AddContactView(AddContactViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}