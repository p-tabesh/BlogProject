using Blog.Application.Model.Comment;

namespace Blog.Application.Service.Comment;

public interface ICommentService
{
    int AddComment(AddCommentRequest request, int requestUserId);
}
