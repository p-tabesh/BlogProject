using Blog.Domain.Entity;
using Blog.Domain.IRepository;
using Blog.Infrastructure.Context;
using Core.Repository.Model.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repository;

public class ArticleRepository : CrudRepository<Article>, IArticleRepository
{
    private readonly BlogDbContext _dbContext;
    private readonly DbSet<Article> _articles;
    public ArticleRepository(BlogDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
        _articles = dbContext.Article;
    }

    public IEnumerable<Article> GetArticlesByCategoryId(int categoryId)
    {
        var articles = _articles.Where(a => a.CategoryId == categoryId).ToList();
        return articles;
    }

    public IEnumerable<Article> GetArticlesByUserId(int userId)
    {
        var articles = _articles.Where(a => a.AuthorUserId == userId).ToList();
        return articles;
    }

    public IEnumerable<Article> GetWithSpecifications(Specification<Article> spec)
    {
        var articles = _articles.Where(spec.Expression);
        return articles;
    }
}
