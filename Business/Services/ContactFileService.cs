using Business.Interfaces;

namespace Business.Services;

public class ContactFileService : FileService, IContactFileService
{
    public ContactFileService(string directoryPath, string fileName) : base(directoryPath, fileName)
    {
    }
}
