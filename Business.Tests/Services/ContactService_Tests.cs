using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;
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

    [Fact]
    public void GetContacts_ShouldReturnListWithSampleContact_IfContactsFileDoesNotExist()
    {
        // arrange
        _contactRepositoryMock
            .Setup(cr => cr.ReadFromFile())
            .Returns((List<ContactModel>?)null);

        // act
        var result = _contactService.GetContacts();

        // assert
            // Can't use Assert.Equal on lists directly since they are two separate lists with different references. Serialize to json and compare strings instead.
        Assert.Equal(JsonSerializer.Serialize(result), JsonSerializer.Serialize(_contactService.GetSampleContacts()));
    }
}
