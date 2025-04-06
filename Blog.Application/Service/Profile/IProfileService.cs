using Blog.Application.Model.Profile;

namespace Blog.Application.Service.Profile;

public interface IProfileService
{
    int CreateProfile(CreateProfileRequest request, int requestUserId);
    void EditProfile(EditProfileRequest request, int requestUserId);
    void ChangeProfileImageLink(ChangeProfileImageLinkRequest request, int requestUserId);
    Domain.Entity.Profile GetProfileByUserId(int requestUserId);
}
