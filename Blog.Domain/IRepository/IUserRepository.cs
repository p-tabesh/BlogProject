using Blog.Domain.Entity;
using Blog.Domain.ValueObject;

namespace Blog.Domain.IRepository;

public interface IUserRepository : ICrudRepository<User>
{
    User GetByUsername(Username username);
    User GetByUsernameAndPassword(Username username, Password password);
    User GetByEmailAndPassword(Email email, Password password);
    IEnumerable<Article> GetFavoriteArticlesByUserId(int userId);
}
