namespace Business.Helpers;

public static class InputParser
{
    public static (T? Parsed, bool ParseSuccess) Parse<T>(string input)
    {
        bool parseSuccess;

        if (typeof(T) == typeof(int))
        {
            if (int.TryParse(input, out int inputParsedToInt))
            {
                parseSuccess = true;
                return ((T)(object)inputParsedToInt, parseSuccess);
            }
            else
            {
                parseSuccess = false;
                return (default(T), parseSuccess);
            }
        }
        else if (typeof(T) == typeof(string))
        {
            parseSuccess = true;
            return ((T)(object)input, parseSuccess);
        }
        else
        {
            parseSuccess = false;
            return (default(T), parseSuccess);
        }
    }
}
