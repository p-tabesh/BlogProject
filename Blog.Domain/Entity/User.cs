using Blog.Domain.Enum;
using Blog.Domain.ValueObject;

namespace Blog.Domain.Entity;

public class User : RootEntity<int>
{
    public Username Username { get; private set; }
    public Password Password { get; private set; }
    public Email Email { get; private set; }
    public DateTime CreationDate { get; private set; }
    public List<int> FavoriteArticleIds { get; private set; } = new();
    public Profile? Profile { get; private set; }

    private User() { }

    public User(Username username, Password password, Email email)
    {
        Username = username;
        Password = password;
        Email = email;
        CreationDate = DateTime.UtcNow;
    }

    public void CreateProfile(string fullName, Gender gender, string birthPlace, string bio, string profileImageLink)
    {
        Profile = new Profile(fullName,gender,birthPlace,bio,profileImageLink,Id);
    }

    public void AddToFavorite(int articleId)
    {
        if (FavoriteArticleIds.Any(a => a == articleId))
            throw new InvalidOperationException("This article already in favorites");
        else
            FavoriteArticleIds.Add(articleId);
    }

    public void RemoveFromFavorite(int articleId)
    {
        if (FavoriteArticleIds.Any(a => a == articleId))
            FavoriteArticleIds.Remove(articleId);
        else
            throw new InvalidOperationException("This article is not in favorites");
    }
}
