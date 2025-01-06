using Business.Interfaces;
using Business.Repositories;
using Business.Services;
using Microsoft.Extensions.Logging;
using Presentation.Maui.ViewModels;

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

        builder.Services.AddSingleton<IContactFileService>(new ContactFileService("Data", "contacts.json"));
        builder.Services.AddSingleton<IContactRepository, ContactRepository>();
        builder.Services.AddSingleton<IContactService, ContactService>();
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<MainPage>();




#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
