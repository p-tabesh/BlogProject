using Blog.Domain.Entity;

namespace Blog.Domain.IRepository;

public interface IArticleRepository : ICrudRepository<Article>
{
    IEnumerable<Article> GetArticlesWithTag(string tag);
    IEnumerable<Article> GetArticlesWithKeyWord(string keyword);
    IEnumerable<Article> GetArticlesByCategoryId(int categoryId);
    IEnumerable<Article> GetArticlesByUserId(int userId);
    IEnumerable<Article> GetAllForAdmin();
}
