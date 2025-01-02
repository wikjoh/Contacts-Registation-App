using Business.Helpers;

namespace Business.Tests.Helpers;

public class InputParser_Tests
{
    [Theory]
    [InlineData("12345", 12345, true)] // valid int
    [InlineData("abc", 0, false)] // invalid int
    [InlineData("", 0, false)] // empty string
    public void Parse_ReturnsInputAsInt_WhenGenericTypeIsInt(string input, int expectedParsed, bool expectedSuccess)
    {
        // act
        var (parsed, success) = InputParser.Parse<int>(input);

        // assert
        Assert.Equal(expectedParsed, parsed);
        Assert.Equal(expectedSuccess, success);
    }


    [Theory]
    [InlineData("Test", "Test", true)] // valid string
    [InlineData("", "", true)] // empty string
    public void Parse_ReturnsInputAsString_WhenGenericTypeIsString(string input, string expectedParsed, bool expectedSuccess)
    {
        // act
        var (parsed, success) = InputParser.Parse<string>(input);

        // assert
        Assert.Equal(expectedParsed, parsed);
        Assert.Equal(expectedSuccess, success);
    }
}
