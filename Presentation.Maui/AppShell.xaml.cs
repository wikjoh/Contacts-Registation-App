using Presentation.Maui.Views;

namespace Presentation.Maui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(ListContactsView), typeof(ListContactsView));
        Routing.RegisterRoute(nameof(AddContactView), typeof(AddContactView));
        Routing.RegisterRoute(nameof(EditContactView), typeof(EditContactView));
    }
}
