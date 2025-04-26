namespace Blog.Application.Model.Comment;

public record CommentViewModel(int Id,string Text,
    bool IsShow,
    DateTime CreationDate,
    int UserId,
    int ArticleId,
    int? RelatedCommentId,
    List<int> Likes,
    List<int> Dislikes,
    List<CommentViewModel> ChildrenComments);



