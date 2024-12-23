using Business.Factories;
using System.ComponentModel.DataAnnotations;

namespace Business.Helpers;

public static class InputValidator
{
    public static List<ValidationResult>? Validate<T>(T input, string propertyName)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(ContactFactory.Create()) { MemberName = propertyName };

        if (Validator.TryValidateProperty(input, context, results))
        {
            return null;
        }
        else return results;
    }
}
