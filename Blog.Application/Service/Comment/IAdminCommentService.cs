using Blog.Application.Model.Comment;

namespace Blog.Application.Service.Comment;

public interface IAdminCommentService
{
    IEnumerable<CommentViewModel> GetAllComments();
    void ShowComment(int commentId);
    void RejectComment(int commentId);
}
