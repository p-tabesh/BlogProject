namespace Blog.Application.Model.Comment;

public record ReplyCommentRequest(int ParentCommentId, string Text);

