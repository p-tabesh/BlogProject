using Blog.Domain.ValueObject;

namespace Blog.Domain.Entity;

public class AdminUser : RootEntity<int>
{
    public Username Username { get; private set; }
    public Password Password { get; private set; }
    public DateTime CreationDate { get; private set; }
    public bool IsActive { get; private set; }
    public List<AdminPermission> Permissions { get; private set; }
}
