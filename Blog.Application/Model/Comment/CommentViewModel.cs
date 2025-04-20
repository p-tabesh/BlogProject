namespace Blog.Application.Model.Comment;

public record CommentViewModel(string Text,
    bool IsShow,
    DateTime CreationDate,
    int UserId,
    int ArticleId,
    int? RelatedCommentId,
    List<int> Likes,
    List<int> Dislikes,
    List<CommentViewModel> ChildrenComments);



