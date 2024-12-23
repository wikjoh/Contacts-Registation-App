using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class ContactDto
{
    [MinLength(2, ErrorMessage = "First name must be at least 2 characters")]
    public string FirstName { get; set; } = null!;

    [MinLength(2, ErrorMessage = "Last name must be at least 2 characters")]
    public string LastName { get; set; } = null!;

    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$", ErrorMessage = "Email must be in format mail@domain.com")]
    public string Email { get; set; } = null!;

    [RegularExpression(@"^\d{5,15}$", ErrorMessage = "Phone number must be at least 5 digits and at most 15 digits long")]
    public string PhoneNumber { get; set; } = null!;

    [MinLength(2, ErrorMessage = "Street address must be at least 2 characters")]
    public string StreetAddress { get; set; } = null!;

    [RegularExpression(@"^\d{5}$", ErrorMessage = "Postal code must be 5 digits")]
    public int PostalCode { get; set; }

    [MinLength(2, ErrorMessage = "City name must be at least 2 characters")]
    public string City { get; set; } = null!;
}
