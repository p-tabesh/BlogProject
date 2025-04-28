using AutoMapper;
using Blog.Application.Model.User;
using Blog.Domain.Entity;
using Blog.Domain.IRepository;
using Blog.Domain.IUnitOfWork;
using Blog.Domain.ValueObject;
using Microsoft.Extensions.Logging;

namespace Blog.Application.Service.User;

public class AdminUserService : BaseService<AdminUserService> IAdminUserService
{
    private readonly IUserRepository _userRepository;
    public AdminUserService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AdminUserService> logger, IUserRepository userRepository)
        : base(unitOfWork, mapper, logger)
    {
        _userRepository = userRepository;
    }

    public void ActiveUser(int userId)
    {
        var user = _userRepository.GetById(userId);
        user.Active();
        _userRepository.Update(user);
        UnitOfWork.Commit();
    }

    public int CreateAdminUser(RegisterRequest request)
    {
        var username = Username.Create(request.Username);
        var password = Password.Create(request.Password);
        var email = Email.Create(request.Email);
        var user = new Domain.Entity.User(username, password, email,true);

        _userRepository.Add(user);
        UnitOfWork.Commit();

        return user.Id;
    }

    public void DeActiveUser(int userId)
    {
        var user = _userRepository.GetById(userId);
        user.DeActive();

        _userRepository.Update(user);
        UnitOfWork.Commit();
    }

    public IEnumerable<Domain.Entity.User> GetAll()
    {
        var users = _userRepository.GetAll();
        return users;
    }
}
