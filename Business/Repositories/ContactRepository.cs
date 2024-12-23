using Business.Interfaces;
using Business.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Business.Repositories;

public class ContactRepository(IContactFileService contactFileService) : IContactRepository
{
    private readonly IContactFileService _contactFileService = contactFileService;


    public string Serialize(List<ContactModel> contacts)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };

        var json = JsonSerializer.Serialize(contacts, options);
        return json;
    }

    public List<ContactModel>? Deserialize(string json)
    {
            var contacts = JsonSerializer.Deserialize<List<ContactModel>>(json);
            return contacts;
    }

    public List<ContactModel>? ReadFromFile()
    {
        var json = _contactFileService.ReadJsonFromFile();

        if (json != null)
        {
            var contacts = Deserialize(json);
            return contacts;
        }
        else
            return null!;
    }

    public bool SaveToFile(List<ContactModel> contacts)
    {
        try
        {
            var json = Serialize(contacts);
            _contactFileService.SaveJsonToFile(json);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to write to file. {ex.Message}");
            return false;
        }
    }
}
