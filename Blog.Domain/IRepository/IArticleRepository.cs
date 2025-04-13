using Blog.Domain.Entity;
using Core.Repository.Model.Specifications;

namespace Blog.Domain.IRepository;

public interface IArticleRepository : ICrudRepository<Article>
{
    IEnumerable<Article> GetArticlesByCategoryId(int categoryId);
    IEnumerable<Article> GetArticlesByUserId(int userId);
    IEnumerable<Article> GetAllForAdmin();
    IEnumerable<Article> GetWithSpecifications(Specification<Article> specification)
}
