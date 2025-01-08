using Presentation.Maui.ViewModels;

namespace Presentation.Maui.Views;

public partial class ListContactsView : ContentPage
{
	public ListContactsView(ListContactsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}