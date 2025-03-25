using Blog.Domain.ValueObject;

namespace Blog.Domain.Entity;

public class User : RootEntity<int>
{
    public Username Username { get; private set; }
    public Password Password { get; private set; }
    public Email Email { get; private set; }
    public DateTime CreationDate { get; private set; }
    public List<Article> FavoriteArticles { get; private set; }
    private User() { }
    public User(Username username, Password password, Email email)
    {
        Username = username;
        Password = password;
        Email = email;
        CreationDate = DateTime.UtcNow;
    }

    public void AddToFavorite(Article article)
    {
        if (FavoriteArticles.Any(a => a.Id == article.Id))
            throw new InvalidOperationException("This article already in favorites");
        else
            FavoriteArticles.Add(article);
    }

    public void RemoveFromFavorite(Article article)
    {
        if (FavoriteArticles.Any(a => a.Id == article.Id))
            FavoriteArticles.Remove(article);
        else
            throw new InvalidOperationException("This article is not in favorites");
    }
}
