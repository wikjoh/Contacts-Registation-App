using Presentation.Maui.ViewModels;

namespace Presentation.Maui.Views;

public partial class EditContactView : ContentPage
{
	public EditContactView(EditContactViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}