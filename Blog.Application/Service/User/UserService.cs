using Blog.Application.Model.User;
using Blog.Domain.IRepository;
using Blog.Domain.IUnitOfWork;
using Blog.Domain.ValueObject;

namespace Blog.Application.Service.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public void AddFavoriteArticle(int requestUserId,int articleId)
    {
        var user = _userRepository.GetById(requestUserId);
    }

    public void ChangePassword(ChangePasswordRequest request)
    {
        
    }

    public void ChangeUsername(ChangeUsernameRequest request)
    {
        
    }

    public int RegisterUser(RegisterRequest request)
    {
        var username = Username.Create(request.Username);
        var password = Password.CreateForRegister(request.Password);
        var email = Email.Create(request.Email);

        var user = new Domain.Entity.User(username, password, email);

        _userRepository.Add(user);
        _unitOfWork.Commit();

        return user.Id;
    }

    public void RemoveFavoriteArtice(int requestUserId,int articleId)
    {
        
    }
}
