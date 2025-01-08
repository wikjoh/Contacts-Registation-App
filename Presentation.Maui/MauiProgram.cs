using Business.Interfaces;
using Business.Repositories;
using Business.Services;
using Microsoft.Extensions.Logging;
using Presentation.Maui.ViewModels;
using Presentation.Maui.Views;

namespace Presentation.Maui;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<IContactFileService>(new ContactFileService(FileSystem.AppDataDirectory, "contacts.json"));
        builder.Services.AddSingleton<IContactRepository, ContactRepository>();
        builder.Services.AddSingleton<IContactService, ContactService>();
        builder.Services.AddSingleton<ListContactsViewModel>();
        builder.Services.AddSingleton<ListContactsView>();

        builder.Services.AddSingleton<AddContactViewModel>();
        builder.Services.AddSingleton<AddContactView>();

        builder.Services.AddSingleton<EditContactViewModel>();
        builder.Services.AddSingleton<EditContactView>();



#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
