using Blog.Application.Model.User;

namespace Blog.Application.Service.User;

public interface IUserService
{
    int RegisterUser(RegisterRequest request);
    void ChangePassword(ChangePasswordRequest request);
    void ChangeUsername(ChangeUsernameRequest request);
}
