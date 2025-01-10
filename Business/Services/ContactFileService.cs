using Business.Interfaces;
using Business.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Business.Services;

public class ContactFileService : FileService, IContactFileService
{
    private readonly string _cfilePath;
    private readonly string _cdirPath;
    public ContactFileService(string directoryPath, string fileName) : base(directoryPath, fileName)
    {
        _cfilePath = Path.Combine(directoryPath, fileName);
        _cdirPath = directoryPath;
    }

    public bool CreateSampleContactsFile_IfContactsFileNotExist()
    {
        List<ContactModel> sampleContact =
        [
            new ContactModel
            {
                Id = 1,
                Guid = new Guid("4b25d304-d5de-475f-8445-a960402bae65"),
                FirstName = "Sample",
                LastName = "Contact",
                Email = "sample.contact@domain.com",
                PhoneNumber = "0700123456",
                StreetAddress = "Sample Street 1",
                PostalCode = 12345,
                City = "Sample City"
            }
        ];


        try
        {
            if (!Directory.Exists(_cdirPath))
            {
                Directory.CreateDirectory(_cdirPath);
            }

            if (!File.Exists(_cfilePath))
            {
                string sampleContactJson = JsonSerializer.Serialize(sampleContact);
                File.WriteAllText(_cfilePath, sampleContactJson);
            }

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to create contacts file with sample. {ex.Message}");
            return false;
        }

    }
}
