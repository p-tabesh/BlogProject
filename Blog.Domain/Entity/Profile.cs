using Blog.Domain.Enum;

namespace Blog.Domain.Entity;

public class Profile: RootEntity<int>
{
    public string FullName { get; private set; }
    public Gender? Gender { get; private set; }
    public string? BirthPlace { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public string? Bio { get; private set; }
    public DateTime CreationDate { get; private set; }
    public User User { get; private set; }
    public int UserId { get; private set; }
}



