namespace Blog.Application.Model.User;

public record ChangePasswordRequest(string OldPassword, string NewPassword);
