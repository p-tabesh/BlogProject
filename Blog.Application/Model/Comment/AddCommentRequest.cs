namespace Blog.Application.Model.Comment;

public record AddCommentRequest(string Text, int ArticleId, int? RelatedCommentId);