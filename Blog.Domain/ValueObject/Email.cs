using System.Text.RegularExpressions;

namespace Blog.Domain.ValueObject;

public class Email
{
    public string Value { get; set; }

    private Email() { }

    public Email(string emailAddress)
    {
        if (!IsValidEmail(emailAddress))
            throw new Exception("Invalid email format.");

        Value =  emailAddress;
    }

    private static bool IsValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}
