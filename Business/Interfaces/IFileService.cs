namespace Business.Interfaces;

public interface IFileService
{
    bool SaveToFile(string content);
    string ReadFromFile();
}
