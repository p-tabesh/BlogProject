using Blog.Domain.Entity;
using Blog.Domain.IRepository;
using Blog.Infrastructure.Context;
using Core.Repository.Model.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repository;

public class CommentRepository : CrudRepository<Comment>, ICommentRepository
{
    public CommentRepository(BlogDbContext dbContext)
        : base(dbContext, e => e.ChildrenComments) { }
    
    public IEnumerable<Comment> GetByArticleId(int articleId)
    {
        var comments = Entities.Where(c => c.ArticleId == articleId && c.IsDeleted == false && c.IsShow == true /*&& c.RelatedCommentId != null*/);
        return comments;
    }

    public IEnumerable<Comment> GetCommentsForAdmin()
    {
        var comments = Entities.ToList();
        return comments;
    }
}
