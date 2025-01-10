namespace Business.Interfaces;

public interface IContactFileService : IFileService
{
    bool CreateSampleContactsFile_IfContactsFileNotExist();
}
