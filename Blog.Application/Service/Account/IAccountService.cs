using Blog.Application.Model.Account;
using System.IdentityModel.Tokens.Jwt;

namespace Blog.Application.Service.Account;

public interface IAccountService
{
    public bool IsTokenBlackListed(string sid);
    public string GetLoginToken(LoginRequest request, bool isAdmin);
    public void Logout(JwtSecurityToken token);    
}

