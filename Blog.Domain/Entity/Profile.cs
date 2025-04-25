using Blog.Domain.Enum;

namespace Blog.Domain.Entity;

public class Profile : RootEntity<int>
{
    public string FullName { get; private set; }
    public Gender? Gender { get; private set; }
    public string? BirthPlace { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public string? Bio { get; private set; }
    public string ProfileImageLink { get; private set; }
    public DateTime CreationDate { get; private set; }

    private Profile() { }
    public Profile(string fullName, Gender gender, string birthPlace, string bio, string profileImageLink, int userId)
    {
        Id = userId;
        FullName = string.IsNullOrEmpty(fullName) ? throw new ArgumentNullException() : fullName;
        Gender = gender;
        BirthPlace = string.IsNullOrEmpty(birthPlace) ? throw new ArgumentNullException() : birthPlace;
        Bio = string.IsNullOrEmpty(bio) ? throw new ArgumentNullException() : bio;
        ProfileImageLink = profileImageLink;
    }

    public void Edit(string fullName, Gender gender, string birthPlace, string bio)
    { 
        FullName = fullName;
        Gender = gender;
        BirthPlace = birthPlace;
        Bio = bio;
    }

    public void ChangeProfileImage(string newProfileImageLink)
    {
        ProfileImageLink = newProfileImageLink;
    }
}



