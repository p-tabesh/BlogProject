using Blog.Domain.Entity;
using Blog.Domain.IRepository;
using Blog.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repository;

public class CommentRepository : CrudRepository<Comment>, ICommentRepository
{
    private BlogDbContext _dbContext;
    private DbSet<Comment> _comments;
    public CommentRepository(BlogDbContext dbContext)
        : base(dbContext, e => e.ChildrenComments)
    {
        _dbContext = dbContext;
        _comments = dbContext.Set<Comment>();
    }

    public IEnumerable<Comment> GetCommentsByArticleId(int articleId)
    {
        var comments = Entities.Where(c => c.ArticleId == articleId && c.IsDeleted == false);
        return comments;
    }

    public IEnumerable<Comment> GetCommentsForAdmin()
    {
        var comments = Entities.ToList();
        return comments;
    }
}
