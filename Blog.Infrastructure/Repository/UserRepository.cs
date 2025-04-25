using Blog.Domain.Entity;
using Blog.Domain.IRepository;
using Blog.Domain.ValueObject;
using Blog.Infrastructure.Context;

namespace Blog.Infrastructure.Repository;


public class UserRepository : CrudRepository<User>, IUserRepository
{
    public UserRepository(BlogDbContext dbContext)
        : base(dbContext, x => x.Profile) { }

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
