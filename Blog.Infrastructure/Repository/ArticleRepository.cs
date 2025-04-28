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


    public IEnumerable<Article> GetSuggestedForUser(int userId)
    {
        var likedByUser = Entities.Where(a => a.Likes.Any(x => x == userId));
        var tags = new List<string>();
        foreach (var a in likedByUser)
            tags.AddRange(a.Tags);

        var articles = Entities.Where(article => article.Tags.Any(tag => tags.Contains(tag)));
        return articles;
    }
}
