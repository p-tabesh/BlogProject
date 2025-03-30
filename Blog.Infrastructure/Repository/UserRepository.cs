using Blog.Domain.Entity;
using Blog.Domain.IRepository;
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

    public User GetByEmailAndPassword(string email, string password)
    {
        var user = _users.FirstOrDefault(u => u.Email.Value == email && u.Password.Value == password);
        var userTest = Entities.FirstOrDefault(u => u.Email.Value == email && u.Password.Value == password);
        return user;
    }

    public User GetByUsername(string username)
    {
        var user = _users.FirstOrDefault(u => u.Username.Value == username);
        return user;
    }

    public User GetByUsernameAndPassword(string username, string password)
    {
        var user = _users.FirstOrDefault(u => u.Username.Value == username && u.Password.Value == password);
        return user;
    }

    public IEnumerable<Article> GetFavoriteArticlesByUserId(int userId)
    {
        var user = _users.Find(userId);
        var favoriteArticles = user?.FavoriteArticles?.ToList();
        return favoriteArticles;
    }
}
