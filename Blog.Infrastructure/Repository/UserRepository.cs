using Blog.Domain.Entity;
using Blog.Domain.IRepository;
using Blog.Domain.ValueObject;
using Blog.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repository;


public class UserRepository : CrudRepository<User>, IUserRepository
{
    public UserRepository(BlogDbContext dbContext)
        : base(dbContext) { }

    public User GetWithProfile(int id)
    {
        var user = Entities.Include(u=>u.Profile).FirstOrDefault(u =>
            u.Id==id);

        return user;
    }
    public User GetByEmailAndPassword(Email email, Password password)
    {
        var user = Entities.FirstOrDefault(u =>
            u.Email.Value == email.Value &&
            u.Password.Value == password.Value);

        return user;
    }

    public User GetByUsername(Username username)
    {
        var user = Entities.FirstOrDefault(u => u.Username.Value == username.Value);
        return user;
    }

    public User GetByUsernameAndPassword(Username username, Password password)
    {
        var user = Entities.FirstOrDefault(u =>
            u.Username.Value == username.Value &&
            u.Password.Value == password.Value);

        return user;
    }
}
