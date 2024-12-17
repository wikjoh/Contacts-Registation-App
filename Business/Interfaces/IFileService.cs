namespace Business.Interfaces;

public interface IFileService
{
    bool SaveJsonToFile(string content);
    string ReadJsonFromFile();
}
