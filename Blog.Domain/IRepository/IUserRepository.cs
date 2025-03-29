using Blog.Domain.Entity;

namespace Blog.Domain.IRepository;

public interface IUserRepository : ICrudRepository<User>
{
    User GetByUsername(string username);
    User GetByUsernameAndPassword(string username, string password);
    User GetByEmailAndPassword(string email, string password);
    IEnumerable<Article> GetFavoriteArticlesByUserId(int userId);
}
