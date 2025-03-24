using Blog.Domain.Enum;
using Blog.Domain.ValueObject;

namespace Blog.Domain.Entity;

public class User : RootEntity<int>
{
    public Username Username { get; private set; }
    public Password Password { get; private set; }
    public Email Email { get; private set; }
    public DateTime CreationDate { get; private set; }

    private User() { }
    public User(Username username, Password password, Email email)
    {
        Username = username;
        Password = password;
        Email = email;
        CreationDate = DateTime.UtcNow;

    }
}
