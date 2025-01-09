using Business.Dtos;
using Business.Helpers;

namespace Business.Tests.Helpers;

public class InputValidator_Tests
{
    [Fact]
    public void Validate_ShouldReturnNull_OnValidationSuccess()
    {
        // Arrange
        var input = "ValidFirstName";
        var propertyName = nameof(ContactDto.FirstName);

        // Act
        var results = InputValidator.Validate(input, propertyName);

        // Assert
        Assert.Null(results);
    }

    [Fact]
    public void Validate_ShouldReturnValidationResults_OnValidationFailure()
    {
        // Arrange
        var input = "X"; //Invalid FirstName, MinLength(2)
        var propertyName = nameof(ContactDto.FirstName);
        // Act
        var results = InputValidator.Validate(input, propertyName);

        // Assert
        Assert.NotNull(results);
        Assert.Equal("First name must be at least 2 characters", results![0].ErrorMessage);
    }
}
