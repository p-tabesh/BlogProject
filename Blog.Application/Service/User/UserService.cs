using AutoMapper;
using Blog.Application.Model.User;
using Blog.Domain.IRepository;
using Blog.Domain.IUnitOfWork;
using Blog.Domain.ValueObject;
using Microsoft.Extensions.Logging;

namespace Blog.Application.Service.User;

public class UserService : BaseService<UserService>, IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, ILogger<UserService> logger, IMapper mapper)
        : base(unitOfWork, mapper, logger)
    {
        _userRepository = userRepository;
    }

    public void ChangePassword(ChangePasswordRequest request, int userId)
    {
        var user = _userRepository.GetById(userId);
        var oldPassword = Password.Create(request.OldPassword);
        var newPassword = Password.Create(request.NewPassword);

        user.ChangePassword(oldPassword, newPassword);
        _userRepository.Update(user);
        UnitOfWork.Commit();
    }

    public void ChangeProfileImageLink(ChangeProfileImageLinkRequest request, int userId)
    {
        var user = _userRepository.GetById(userId);
        user.ChangeProfileImageLink(request.ImageLink);
        _userRepository.Update(user);
        UnitOfWork.Commit();
    }

    public void ChangeUsername(ChangeUsernameRequest request, int userId)
    {
        var username = Username.Create(request.NewUsername);

        if (_userRepository.GetByUsername(username) != null)
            throw new Exception("this username already exists");

        var user = _userRepository.GetById(userId);
        user.ChangeUsername(username);
        _userRepository.Update(user);
        UnitOfWork.Commit();
    }

    public void CreateProfile(CreateProfileRequest request, int userId)
    {
        var user = _userRepository.GetById(userId);
        user.CreateProfile(request.FullName, request.Gender, request.BirthPlace, request.Bio, request.ProfileImageLink);
        _userRepository.Update(user);
        UnitOfWork.Commit();
    }

    public void EditProfile(EditProfileRequest request, int userId)
    {
        var user = _userRepository.GetById(userId);
        user.EditProfile(request.FullName, request.Gender, request.BirthPlace, request.Bio);
        _userRepository.Update(user);
        UnitOfWork.Commit();
    }

    public ProfileViewModel GetUserProfile(int userId)
    {
        var user = _userRepository.GetById(userId);

        if (user?.Profile == null)
            throw new Exception("user doesn't have profile");

        var model = Mapper.Map<ProfileViewModel>(user);
        return model;
    }

    public int RegisterUser(RegisterRequest request)
    {
        var username = Username.Create(request.Username);

        if (_userRepository.GetByUsername(username) != null)
            throw new Exception("this username already exists ");

        var password = Password.Create(request.Password);
        var email = Email.Create(request.Email);

        var user = new Domain.Entity.User(username, password, email);

        _userRepository.Add(user);
        UnitOfWork.Commit();

        return user.Id;
    }
}
