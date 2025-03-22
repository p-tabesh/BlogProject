using Blog.Domain.ValueObject;

namespace Blog.Domain.Entity;

public class User : RootEntity<int>
{
    public string UserTitle { get; private set; }
    public Username Username { get; private set; }
    public Password Password { get; private set; }
    public Email Email { get; private set; }
    public DateTime CreationDate { get; private set; }

    private User() { }
    public User(string userTitle, Username username, Password password, Email email)
    {
        UserTitle = userTitle;
        Username = username;
        Password = password;
        Email = email;
        CreationDate = DateTime.UtcNow;
    }
}
