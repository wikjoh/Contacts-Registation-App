using Business.Helpers;

namespace Business.Tests.Helpers;

public class InputParser_Tests
{
    [Theory]
    [InlineData("12345", 12345, true)] // valid int
    [InlineData("abc", 0, false)] // invalid int
    [InlineData("", 0, false)] // empty string
    public void Parse_ShouldReturnInputAsInt_WhenGenericTypeIsInt(string input, int expectedParsed, bool expectedSuccess)
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
    public void Parse_ShouldReturnInputAsString_WhenGenericTypeIsString(string input, string expectedParsed, bool expectedSuccess)
    {
        // act
        var (parsed, success) = InputParser.Parse<string>(input);

        // assert
        Assert.Equal(expectedParsed, parsed);
        Assert.Equal(expectedSuccess, success);
    }


    [Fact]
    public void Parse_ShouldReturnDefaultAndFalse_WhenGenericTypeIsUnsupported()
    {
        // arrange
        string inputDecimal = "123.456m";
        string inputFloat = "123.4f";
        string inputBool = "true";
        string inputLong = "1234567890L";
        string inputDouble = "9.99D";
        string inputChar = "A";

        // act
        var resultDecimal = InputParser.Parse<decimal>(inputDecimal);
        var resultFloat = InputParser.Parse<float>(inputFloat);
        var resultBool = InputParser.Parse<bool>(inputBool);
        var resultLong = InputParser.Parse<long>(inputLong);
        var resultDouble = InputParser.Parse<double>(inputDouble);
        var resultChar = InputParser.Parse<char>(inputChar);

        // assert
        Assert.True(resultDecimal.Parsed == default(decimal) && !resultDecimal.ParseSuccess);
        Assert.True(resultFloat.Parsed == default(float) && !resultFloat.ParseSuccess);
        Assert.True(resultBool.Parsed == default(bool) && !resultBool.ParseSuccess);
        Assert.True(resultLong.Parsed == default(long) && !resultLong.ParseSuccess);
        Assert.True(resultDouble.Parsed == default(double) && !resultDouble.ParseSuccess);
        Assert.True(resultChar.Parsed == default(char) && !resultChar.ParseSuccess);
    }
}
