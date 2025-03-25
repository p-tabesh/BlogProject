using Blog.Domain.Entity;

namespace Blog.Domain.IRepository;

public interface IArticleRepository : ICrudRepository<Article>
{

}
