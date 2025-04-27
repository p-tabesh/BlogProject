

namespace Blog.Application.Model.Comment;

public class CommentMapper
{
    public static Domain.Entity.Comment MapToEntity(AddCommentRequest request, int requestUserId)
    {
        var comment = new Domain.Entity.Comment(request.Text, request.RelatedCommentId, request.ArticleId, requestUserId);
        return comment;
    }

    public static CommentViewModel MapFromEntity(Domain.Entity.Comment comment)
    {
        var model = new CommentViewModel(comment.Id,
            comment.Text,
            comment.IsShow,
            comment.CreationDate,
            comment.UserId,
            comment.ArticleId,
            comment.RelatedCommentId,
            comment.Likes,
            comment.Dislikes, comment.ChildrenComments?.Select(x => MapFromEntity(x)).ToList());
        return model;
    }
}
