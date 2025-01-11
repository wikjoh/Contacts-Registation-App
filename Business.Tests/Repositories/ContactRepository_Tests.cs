using Business.Interfaces;
using Business.Models;
using Business.Repositories;
using Moq;
using System.Text.Json;

namespace Business.Tests.Repositories;

public class ContactRepository_Tests
{
    private readonly Mock<IContactFileService> _contactFileServiceMock;
    private readonly IContactRepository _contactRepository;

    public ContactRepository_Tests()
    {
        _contactFileServiceMock = new Mock<IContactFileService>();
        _contactRepository = new ContactRepository(_contactFileServiceMock.Object);
    }


    [Fact]
    public void Serialize_ShouldReturnParameterAsJson()
    {
        // arrange
        List<ContactModel> contactList = [new ContactModel()];

        // act
        var result = _contactRepository.Serialize(contactList);

        // assert
        Assert.Equal(result, JsonSerializer.Serialize(contactList, new JsonSerializerOptions{ WriteIndented = true }));
    }


    [Fact]
    public void Deserialize_ShouldReturnJsonAsContactModelList()
    {
        // arrange
        string contactListJson = "[{\"Id\":0,\"Guid\":\"5ff251ce-94ec-4259-9b47-cf3e09c50058\",\"FirstName\":null,\"LastName\":null,\"Email\":null,\"PhoneNumber\":null,\"StreetAddress\":null,\"PostalCode\":null,\"City\":null}]";

        // act
        var result = _contactRepository.Deserialize(contactListJson);

        // assert
        var expected = JsonSerializer.Deserialize<List<ContactModel>>(contactListJson);

        Assert.Equal(expected![0].Id, result![0].Id);
        Assert.Equal(expected[0].Guid, result[0].Guid);
        Assert.Equal(expected[0].FirstName, result[0].FirstName);
        Assert.Equal(expected[0].LastName, result[0].LastName);
        Assert.Equal(expected[0].Email, result[0].Email);
        Assert.Equal(expected[0].PhoneNumber, result[0].PhoneNumber);
        Assert.Equal(expected[0].StreetAddress, result[0].StreetAddress);
        Assert.Equal(expected[0].PostalCode, result[0].PostalCode);
        Assert.Equal(expected[0].City, result[0].City);
            
    }


    [Fact]
    public void ReadFromFile_ShouldReturnContactModelList_IfSuccessful()
    {
        // arrange
        _contactFileServiceMock
            .Setup(cfs => cfs.ReadJsonFromFile())
            .Returns("[{\"Id\":0,\"Guid\":\"5ff251ce-94ec-4259-9b47-cf3e09c50058\",\"FirstName\":null,\"LastName\":null,\"Email\":null,\"PhoneNumber\":null,\"StreetAddress\":null,\"PostalCode\":null,\"City\":null}]");

        // act
        var result = _contactRepository.ReadFromFile();

        // assert
        var expected = new List<ContactModel> { new ContactModel { Guid = new Guid("5ff251ce-94ec-4259-9b47-cf3e09c50058") } };

        Assert.Equal(expected![0].Id, result![0].Id);
        Assert.Equal(expected[0].Guid, result[0].Guid);
        Assert.Equal(expected[0].FirstName, result[0].FirstName);
        Assert.Equal(expected[0].LastName, result[0].LastName);
        Assert.Equal(expected[0].Email, result[0].Email);
        Assert.Equal(expected[0].PhoneNumber, result[0].PhoneNumber);
        Assert.Equal(expected[0].StreetAddress, result[0].StreetAddress);
        Assert.Equal(expected[0].PostalCode, result[0].PostalCode);
        Assert.Equal(expected[0].City, result[0].City);
    }


    [Fact]
    public void ReadFromFile_ShouldReturnNull_IfFileIsEmpty()
    {
        // arrange
        _contactFileServiceMock
            .Setup(cfs => cfs.ReadJsonFromFile())
            .Returns((string)null!);

        // act
        var result = _contactRepository.ReadFromFile();

        // assert
        Assert.Null(result);
    }


    [Fact]
    public void SaveToFile_ShouldReturnTrue_IfSuccessful()
    {
        // arrange
        _contactFileServiceMock
            .Setup(cfs => cfs.SaveJsonToFile(It.IsAny<string>()))
            .Returns(true);

        // act
        List<ContactModel> contacts = [new ContactModel()];
        var result = _contactRepository.SaveToFile(contacts);

        // assert
        Assert.True(result);
    }


    [Fact]
    public void SaveToFile_ShouldHandleException_AndReturnFalse()
    {
        // arrange
        _contactFileServiceMock
            .Setup(cfs => cfs.SaveJsonToFile(It.IsAny<string>()))
            .Throws(new Exception("Mocktest"));

        // act
        List<ContactModel> contacts = [new ContactModel()];
        var result = _contactRepository.SaveToFile(contacts);

        // assert
        Assert.False(result);
    }
}
