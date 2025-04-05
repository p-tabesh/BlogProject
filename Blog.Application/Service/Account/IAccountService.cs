using Blog.Application.Model.Account;
using Blog.Domain.Entity;

namespace Blog.Application.Service.Account;

public interface IAccountService
{
    public User GetUser(LoginRequest request);
    public bool IsTokenBlackListed(string sid);
    public string GenerateToken(int userId);
}
