using Blog.Application.Model.User;

namespace Blog.Application.Service.User;

public interface IUserService
{
    int RegisterUser(RegisterRequest request);
    void ChangePassword(ChangePasswordRequest request, int requestUserId);
    void ChangeUsername(ChangeUsernameRequest request, int requstUserId);
    void CreateProfile(CreateProfileRequest request, int requestUserId);
    void EditProfile(EditProfileRequest request, int requestUserId);
    void ChangeProfileImageLink(ChangeProfileImageLinkRequest request, int requestUserId);
    ProfileViewModel GetUserProfile(int userId);
}
