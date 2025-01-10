using Business.Interfaces;
using Business.Repositories;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Console_MainApp.Dialogs;

namespace Presentation.Console_MainApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IContactFileService>(new ContactFileService("Data", "contacts.json"))
                .AddSingleton<IContactRepository, ContactRepository>()
                .AddSingleton<IContactService, ContactService>()
                .AddTransient<MenuDialog>()
                .BuildServiceProvider();

            var cfs = serviceProvider.GetRequiredService<IContactFileService>();
            cfs.CreateSampleContactsFile_IfContactsFileNotExist();

            var menuDialog = serviceProvider.GetRequiredService<MenuDialog>();
            menuDialog.ShowMainMenu();
        }
    }
}
