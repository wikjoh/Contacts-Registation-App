using Business.Dtos;
using Business.Factories;
using Business.Models;

namespace Business.Tests.Factories;

public class ContactFactory_Tests
{
    [Fact]
    public void Create_ShouldReturnContactDto()
    {
        // arrange

        // act
        ContactDto result = ContactFactory.Create();

        // assert
        Assert.NotNull(result);
        Assert.IsType<ContactDto>(result);
    }

    [Fact]
    public void Create_ShouldReturnContactModel_WhenContactDtoIsProvided()
    {
        // arrange
        ContactDto ContactDto = new()
        {
            FirstName = "TestFirstName1",
            LastName = "TestLastName1",
            Email = "1@test.com",
            PhoneNumber = "0700000001",
            StreetAddress = "Testvägen 1",
            PostalCode = 12345,
            City = "Teststad 1"
        };

        // act
        ContactModel result = ContactFactory.Create(ContactDto)!;

        // assert
        Assert.NotNull(result);
        Assert.IsType<ContactModel>(result);
        Assert.Equal(0, result.Id);
        Assert.True(result.Guid.GetType() == typeof(Guid) && result.Guid.ToString().Length == 36);
        Assert.Equal(ContactDto.FirstName, result.FirstName);
        Assert.Equal(ContactDto.LastName, result.LastName);
        Assert.Equal(ContactDto.Email, result.Email);
        Assert.Equal(ContactDto.PhoneNumber, result.PhoneNumber);
        Assert.Equal(ContactDto.StreetAddress, result.StreetAddress);
        Assert.Equal(ContactDto.PostalCode, result.PostalCode);
        Assert.Equal(ContactDto.City, result.City);
    }
}
