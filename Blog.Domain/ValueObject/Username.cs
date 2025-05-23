namespace Blog.Domain.ValueObject;

public class Username : BaseValueObject
{
    private Username() { }

    private Username(string username)
    {
        if (!IsValidUsername(username))
            throw new ArgumentException("Username invalid");

        Value = username;
    }

    public static Username Create(string username)
    {
        var generatedUsername = new Username(username);
        return generatedUsername;
    }

    private static bool IsValidUsername(string username)
    {
        if (string.IsNullOrEmpty(username) ||
                username.Length < 4 ||
                username.Length > 20)
            return false;

        return true;
    }
}
