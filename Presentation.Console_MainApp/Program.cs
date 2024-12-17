using Business.Interfaces;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Console_MainApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IContactFileService>(new ContactFileService("Data", "contacts.json"))
                .BuildServiceProvider();
        }
    }
}
