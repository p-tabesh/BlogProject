using Blog.Application.Model.Profile;
using Blog.Domain.IRepository;
using Blog.Domain.IUnitOfWork;

namespace Blog.Application.Service.Profile;

public class ProfileService : IProfileService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProfileRepository _profileRepository;
    public ProfileService(IUnitOfWork unitOfWork, IProfileRepository profileRepository)
    {
        _unitOfWork = unitOfWork;
        _profileRepository = profileRepository;
    }
    public int CreateProfile(CreateProfileRequest request, int requestUserId)
    {
        var profile = new Domain.Entity.Profile(request.FullName, request.Gender, request.BirthPlace, request.Bio, request.ProfileImageLink, requestUserId);
        _profileRepository.Add(profile);    
        _unitOfWork.Commit();

        return profile.Id;
    }

    public void EditProfile(EditProfileRequest request, int requestUserId)
    {
        var profile = _profileRepository.GetByUserId(requestUserId);

        if (profile == null)
            throw new Exception("User doesn't have profile");

        profile.EditProfile(request.FullName, request.Gender, request.BirthPlace, request.Bio);

        _profileRepository.Update(profile);
        _unitOfWork.Commit();
    }

    public void ChangeProfileImageLink(ChangeProfileImageLinkRequest request , int requestUserId)
    {
        var profile = _profileRepository.GetByUserId(requestUserId);

        if (profile == null)
            throw new Exception("User doesn't have profile");

        profile.ChangeProfileImage(request.ImageLink);

        _profileRepository.Update(profile);
        _unitOfWork.Commit();
    }

    public Domain.Entity.Profile GetProfileByUserId(int requestUserId)
    {
        var profile = _profileRepository.GetByUserId(requestUserId);
        if (profile == null)
            throw new Exception("User doesn't have profile");

        return profile;
    }
}
