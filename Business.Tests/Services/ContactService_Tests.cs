using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;
using System.Diagnostics;
using System.Text.Json;

namespace Business.Tests.Services;

public class ContactService_Tests
{
    private readonly Mock<IContactRepository> _contactRepositoryMock;
    private readonly IContactService _contactService;

    public ContactService_Tests()
    {
        _contactRepositoryMock = new Mock<IContactRepository>();
        _contactService = new ContactService(_contactRepositoryMock.Object);
    }


    //GetContacts Tests
    [Fact]
    public void GetContacts_ShouldReturnEmptyList_IfContactsFileDoesNotExist()
    {
        // arrange
        _contactRepositoryMock
            .Setup(cr => cr.ReadFromFile())
            .Returns((List<ContactModel>?)null);

        // act
        var result = _contactService.GetContacts();

        // assert
        Assert.Equal(result, []);
    }


    [Fact]
    public void GetContacts_ShouldReturnListOfContacts_IfContactsFileContainsContacts()
    {
        // arrange
        List<ContactModel> mockedContactsListFromFile = [
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
            },
            new ContactModel
            {
                Id = 2,
                Guid = new Guid("4b25d304-d5de-475f-8445-a960402bae66"),
                FirstName = "Sample2",
                LastName = "Contact2",
                Email = "sample.contact@domain.com",
                PhoneNumber = "0700123456",
                StreetAddress = "Sample Street 1",
                PostalCode = 12345,
                City = "Sample City"
            }
        ];

        _contactRepositoryMock
            .Setup(cr => cr.ReadFromFile())
            .Returns(mockedContactsListFromFile);

        // act
        var result = _contactService.GetContacts();

        // assert
        // Can't use Assert.Equal on lists directly since they are two separate lists with different references. Serialize to json and compare strings instead.
        Assert.Equal(JsonSerializer.Serialize(result), JsonSerializer.Serialize(mockedContactsListFromFile));
    }


    [Fact]
    public void GetContacts_ShouldReturnEmptyList_IfContactsFileIsEmpty()
    {
        // arrange
        List<ContactModel> mockedContactsListFromFile = [];

        _contactRepositoryMock
            .Setup(cr => cr.ReadFromFile())
            .Returns(mockedContactsListFromFile);

        // act
        var result = _contactService.GetContacts();

        // assert
        Assert.Equal(result, []);
    }


    // CreateContact Tests
    [Fact]
    public void CreateContact_ShouldReturnTrue_WhenContactIsCreatedSuccessfully()
    {
        // arrange
        ContactDto sampleDto = new();

        _contactRepositoryMock.Setup(cr => cr.SaveToFile(It.IsAny<List<ContactModel>>())).Returns(true);

        // act
        var result = _contactService.CreateContact(sampleDto);

        // assert
        Assert.True(result);
        Assert.Single(_contactService.GetContactsFromMemory());
    }


    [Fact]
    public void CreateContact_ShouldReturnFalse_WhenSuppliedParameterIsNull()
    {
        // arrange

        // act
        var result = _contactService.CreateContact(null!);

        // assert
        Assert.False(result);
    }


    [Fact]
    public void CreateContact_ShouldHandleException_AndReturnFalse()
    {
        // arrange
        _contactRepositoryMock
            .Setup(cr => cr.SaveToFile(It.IsAny<List<ContactModel>>()))
            .Throws(new Exception("Mocktest"));

        // act
        var result = _contactService.CreateContact(new ContactDto());

        // assert
        Assert.False(result);
    }


    [Fact]
    public void CreateContact_ShouldInvokeContactsUpdated_OnSuccess()
    {
        // arrange
        _contactRepositoryMock
            .Setup(cr => cr.SaveToFile(It.IsAny<List<ContactModel>>()))
            .Returns(true);

        bool contactsUpdatedInvoked = false;
        _contactService.ContactsUpdated += (sender, args) => contactsUpdatedInvoked = true;

        // act
        _contactService.CreateContact(new ContactDto());

        // assert
        Assert.True(contactsUpdatedInvoked);
    }


    // DeleteContact Tests
    [Fact]
    public void DeleteContact_ShouldReturnTrue_IfContactFoundAndDeleted()
    {
        // arrange
        _contactService.CreateContact(new ContactDto()); // Adds contact to list with id 1
        _contactRepositoryMock.Setup(cr => cr.SaveToFile(It.IsAny<List<ContactModel>>()))
            .Returns(true); // Mock SaveToFile() in order for CreateContact() to work

        // act
        var result = _contactService.DeleteContact(1);

        // assert
        Assert.True(result);
        Assert.Empty(_contactService.GetContacts());
    }


    [Fact]
    public void DeleteContact_ShouldReturnFalse_IfContactNotFound()
    {
        // arrange

        // act
        var result = _contactService.DeleteContact(1337);

        // assert
        Assert.False(result);
    }


    [Fact]
    public void DeleteContact_ShouldInvokeContactsUpdated_OnSuccess()
    {
        // arrange
        _contactService.CreateContact(new ContactDto()); // Adds contact to list with id 1
        _contactRepositoryMock.Setup(cr => cr.SaveToFile(It.IsAny<List<ContactModel>>()))
            .Returns(true); // Mock SaveToFile() in order for CreateContact() to work

        bool contactsUpdatedInvoked = false;
        _contactService.ContactsUpdated += (sender, args) => contactsUpdatedInvoked = true;

        // act
        var result = _contactService.DeleteContact(1);

        // assert
        Assert.True(contactsUpdatedInvoked);
    }


    [Fact]
    public void DeleteContact_ShouldHandleException_AndReturnFalse()
    {
        // arrange
        _contactService.CreateContact(new ContactDto()); // Adds contact to list with id 1
        _contactRepositoryMock.Setup(cr => cr.SaveToFile(It.IsAny<List<ContactModel>>()))
            .Returns(true); // Mock SaveToFile() in order for CreateContact() to work

        _contactRepositoryMock
            .Setup(cr => cr.SaveToFile(It.IsAny<List<ContactModel>>()))
            .Throws(new Exception("Mocktest"));

        // act
        var result = _contactService.DeleteContact(1);

        // assert
        Assert.False(result);
    }



    // UpdateContact Tests
    [Fact]
    public void UpdateContact_ShouldUpdateContact_AndReturnTrue()
    {
        // arrange
        _contactService.CreateContact(new ContactDto()); // Adds contact to list with id 1
        _contactRepositoryMock.Setup(cr => cr.SaveToFile(It.IsAny<List<ContactModel>>()))
            .Returns(true); // Mock SaveToFile() in order for CreateContact() to work

        // act
        var result = _contactService.UpdateContact(new ContactModel { Id = 1, FirstName = "Mocktest" });

        // assert
        Assert.True(result);
        Assert.Equal("Mocktest", _contactService.GetContactsFromMemory().First().FirstName);
    }


    [Fact]
    public void UpdateContact_ShouldReturnFalse_IfContactNotFound()
    {
        // arrange
        ContactModel contact = new ContactModel { Id = 1 };

        // act
        var result = _contactService.UpdateContact(contact);

        // assert
        Assert.False(result);
    }


    [Fact]
    public void UpdateContact_ShouldReturnFalse_IfParameterIsNull()
    {
        // arrange

        // act
        var result = _contactService.UpdateContact(null!);

        // assert
        Assert.False(result);
    }


    [Fact]
    public void UpdateContact_ShouldInvokeContactsUpdated_OnSuccess()
    {
        // arrange
        _contactService.CreateContact(new ContactDto()); // Adds contact to list with id 1
        _contactRepositoryMock.Setup(cr => cr.SaveToFile(It.IsAny<List<ContactModel>>()))
            .Returns(true); // Mock SaveToFile() in order for CreateContact() to work

        bool contactsUpdatedInvoked = false;
        _contactService.ContactsUpdated += (sender, args) => contactsUpdatedInvoked = true;

        // act
        var result = _contactService.UpdateContact(new ContactModel { Id = 1, FirstName = "Mocktest" });


        // assert
        Assert.True(contactsUpdatedInvoked);
    }


    [Fact]
    public void UpdateContact_ShouldHandleException_AndReturnFalse()
    {
        // arrange
        _contactService.CreateContact(new ContactDto()); // Adds contact to list with id 1
        _contactRepositoryMock.Setup(cr => cr.SaveToFile(It.IsAny<List<ContactModel>>()))
            .Returns(true); // Mock SaveToFile() in order for CreateContact() to work

        _contactRepositoryMock
            .Setup(cr => cr.SaveToFile(It.IsAny<List<ContactModel>>()))
            .Throws(new Exception("Mocktest"));

        // act
        var result = _contactService.UpdateContact(new ContactModel { Id = 1, FirstName = "Mocktest" });

        // assert
        Assert.False(result);
    }
}
