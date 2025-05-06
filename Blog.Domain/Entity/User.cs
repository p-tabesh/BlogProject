using Blog.Domain.Enum;
using Blog.Domain.ValueObject;

namespace Blog.Domain.Entity;

public class User : RootEntity<int>
{
    public Username Username { get; private set; }
    public Password Password { get; private set; }
    public Email Email { get; private set; }
    public bool IsAdmin { get; private set; }
    public DateTime CreationDate { get; private set; }
    public List<int> FavoriteArticleIds { get; private set; } = new();
    public Profile? Profile { get; private set; }
    public bool IsActive { get; private set; }

    private User() { }

    public User(Username username, Password password, Email email, bool isAdmin)
    {
        Username = username;
        Password = password;
        Email = email;
        IsAdmin = isAdmin;
        CreationDate = DateTime.UtcNow;
        IsActive = true;
    }

    public void ChangeUsername(Username username)
    {
        Username = username;
    }

    public void ChangePassword(Password oldPassword, Password newPassword)
    {
        if (Password != oldPassword)
            throw new Exception("old password doesn't match");

        Password = newPassword;
    }

    public void CreateProfile(string fullName, Gender gender, string birthPlace, string bio, string profileImageLink)
    {
        Profile = new Profile(fullName, gender, birthPlace, bio, profileImageLink, Id);
    }

    public void EditProfile(string fullName, Gender gender, string birthPlace, string bio)
    {
        if (Profile == null)
            throw new Exception("User doesn't have profile");

        Profile.Edit(fullName, gender, birthPlace, bio);
    }

    public void ChangeProfileImageLink(string imageLink)
    {
        if (Profile == null)
            throw new Exception("User doesn't have profile");

        Profile.ChangeProfileImage(imageLink);
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

    public void Active()
    {
        if (!IsActive)
            IsActive = true;
        return;
    }

    public void DeActive()
    {
        if (IsActive)
            IsActive = false;
        return;
    }
}
