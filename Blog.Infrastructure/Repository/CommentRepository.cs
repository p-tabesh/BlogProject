using Blog.Domain.Entity;
using Blog.Domain.IRepository;
using Blog.Infrastructure.Context;
using Blog.Infrastructure.Extention;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repository;

public class CommentRepository : CrudRepository<Comment>, ICommentRepository
{
    private BlogDbContext _dbContext;
    private DbSet<Comment> _comments;
    public CommentRepository(BlogDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _comments = dbContext.Set<Comment>();
    }

    public IEnumerable<Comment> GetCommentsByArticleId(int articleId)
    {
        var comments = _comments.IncludeMultiple(c => c.ChildrenComments).Where(c => c.ArticleId == articleId);
        return comments;
    }
}
