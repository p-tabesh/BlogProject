using Blog.Domain.Entity;
using Blog.Domain.IRepository;
using Blog.Domain.ValueObject;
using Blog.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repository;

// پرایوت تو همش نداشته باشیم و از پراپتری انتیتیز توی کلاس پدر استفاده کنیم؟ 
// و یک اورلود از سازنده در کلاس پدر برای اینکلود کردن پراپتری های مورد نیاز فرزند؟
public class UserRepository : CrudRepository<User>, IUserRepository
{
    private readonly BlogDbContext _dbContext;
    private readonly DbSet<User> _users;
    public UserRepository(BlogDbContext dbContext)
        : base(dbContext, x => x.Profile)
    {
        _dbContext = dbContext;
        _users = dbContext.User;
    }

    public User GetByEmailAndPassword(Email email, Password password)
    {
        var user = _users.FirstOrDefault(u => 
            u.Email.Value == email.Value && 
            u.Password.Value == password.Value);

        return user;
    }

    public User GetByUsername(Username username)
    {
        var user = _users.FirstOrDefault(u => u.Username.Value == username.Value);
        return user;
    }

    public User GetByUsernameAndPassword(Username username, Password password)
    {
        var user = _users.FirstOrDefault(u =>
            u.Username.Value == username.Value &&
            u.Password.Value == password.Value);

        return user;
    }
}
