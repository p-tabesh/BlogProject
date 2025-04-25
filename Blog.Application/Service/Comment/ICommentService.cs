using Blog.Application.Model.Comment;

namespace Blog.Application.Service.Comment;

public interface ICommentService
{
    int AddComment(AddCommentRequest request, int requestUserId);
    void ReplyComment(ReplyCommentRequest request, int requestUserId);
    IEnumerable<CommentViewModel> GetCommentsByArticleId(int articleId);
    void LikeComment(int commentId, int userId);
    void DislikeCommet(int commentId, int userId);
}
