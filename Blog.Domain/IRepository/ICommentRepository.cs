using Blog.Domain.Entity;

namespace Blog.Domain.IRepository;

internal interface ICommentRepository : ICrudRepository<Comment>
{

}
