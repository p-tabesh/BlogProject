namespace Blog.Domain.ValueObject;

public class Username
{
    public string Value { get; private set; }

    private Username() { }

    public Username(string username)
    {
        if (!IsValidUsername(username))
            throw new ArgumentException("Username invalid");

        Value = username;

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
