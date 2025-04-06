using Blog.Application.Model.Account;
using Blog.Domain.Entity;
using Blog.Domain.IRepository;
using Blog.Domain.ValueObject;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.Application.Service.Account;

public class AccountService : IAccountService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public AccountService(IConfiguration configuration, IUserRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public bool IsTokenBlackListed(string sid)
    {
        return true;
    }

    public string GenerateToken(int userId)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        securityKey.KeyId = Guid.NewGuid().ToString();
        //var role = isAdmin == false ? null : "admin";
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, Convert.ToString(userId)),
            new Claim(ClaimTypes.Sid,Guid.NewGuid().ToString())
        };

        //if (isAdmin)
        //{
        //    claims.Add(new Claim(ClaimTypes.Role, role));
        //}

        var token = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(Convert.ToInt32(_configuration["Jwt:TokenLifeTime"])),
            signingCredentials: credentials);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenString;
    }

    public Domain.Entity.User GetUser(LoginRequest request)
    {
        var username = new Username(request.Username);
        var password = Password.CreateForLogin(request.Password);
        var user = _userRepository.GetByUsernameAndPassword(username, password);

        if (user == null)
            throw new Exception("invalid username or password");

        return user;
    }
}
