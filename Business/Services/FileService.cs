using Business.Interfaces;
using System.Diagnostics;

namespace Business.Services;

public abstract class FileService : IFileService
{
    private readonly string _filePath;
    private readonly string _dirPath;

    public FileService(string directoryPath, string fileName)
    {
        _filePath = Path.Combine(directoryPath, fileName);
        _dirPath = directoryPath;
    }


    public virtual string ReadFromFile()
    {
        if (File.Exists(_filePath))
        {
            return File.ReadAllText(_filePath);
        }

        return null!;
    }

    public virtual bool SaveToFile(string content)
    {
        try
        {
            if (!Directory.Exists(_dirPath))
            {
                Directory.CreateDirectory(_dirPath);
            }

            File.WriteAllText(_filePath, content);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }
}
