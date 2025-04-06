using System.Text.RegularExpressions;

namespace Blog.Domain.ValueObject;

public class Email
{
    public string Value { get; private set; }

    private Email() { }

    private Email(string emailAddress)
    {
        if (!IsValidEmail(emailAddress))
            throw new Exception("Invalid email format.");

        Value =  emailAddress;
    }

    public static Email Create(string emailAddress)
    {
        var email = new Email(emailAddress);
        return email;
    }

    private static bool IsValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}
