using Business.Models;

namespace Business.Interfaces;

public interface IContactRepository
{
    List<ContactModel>? Deserialize(string json);
    List<ContactModel>? ReadFromFile();
    bool SaveToFile(List<ContactModel> contacts);
    string Serialize(List<ContactModel> contacts);
}
