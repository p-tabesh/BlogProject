using Blog.Domain.Entity;
using Blog.Domain.IRepository;
using Blog.Infrastructure.Context;
using Core.Repository.Model.Specifications;
using Microsoft.EntityFrameworkCore;
using Blog.Infrastructure.Extention;

namespace Blog.Infrastructure.Repository;

public class ArticleRepository : CrudRepository<Article>, IArticleRepository
{
    public ArticleRepository(BlogDbContext dbContext)
        : base(dbContext) { }

    public IEnumerable<Article> GetArticlesByCategoryId(int categoryId)
    {
        var articles = Entities.Where(a => a.CategoryId == categoryId).ToList();
        return articles;
    }

    public IEnumerable<Article> GetArticlesByUserId(int userId)
    {
        var articles = Entities.Where(a => a.AuthorUserId == userId).ToList();
        return articles;
    }

    public IEnumerable<Article> GetWithSpecifications(Specification<Article> specification)
    {
        var articles = Entities.Where(specification);
        return articles;
    }
}
