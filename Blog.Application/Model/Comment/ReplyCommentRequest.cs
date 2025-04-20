namespace Blog.Application.Model.Comment;

public record ReplyCommentRequest(int RelatedComment, string Text);

