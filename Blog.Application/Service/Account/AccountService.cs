using Blog.Application.Model.Account;
using Blog.Domain.IRepository;
using Blog.Domain.ValueObject;
using Microsoft.Extensions.Caching.Distributed;
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
    private readonly IDistributedCache _redisCache;

    public AccountService(IConfiguration configuration, IUserRepository userRepository, IDistributedCache redisCache)
    {
        _configuration = configuration;
        _userRepository = userRepository;
        _redisCache = redisCache;
    }

    public string GetLoginToken(LoginRequest request, bool isAdmin)
    {
        var user = GetUser(request);

        if (isAdmin && !user.IsAdmin)
            throw new Exception("invlied username or password");

        var credentials = GetCredentials();
        var claims = GetClaims(user);
        var token = GenerateToken(claims, credentials);

        return token;
    }

    public void Logout(JwtSecurityToken token)
    {
        var userId = token.Claims.FirstOrDefault(t => t.Type == ClaimTypes.NameIdentifier)?.Value;
        var sid = token.Claims.FirstOrDefault(t => t.Type == ClaimTypes.Sid)?.Value;

        _redisCache.SetString(
            sid,
            userId,
            new DistributedCacheEntryOptions() { AbsoluteExpiration = DateTime.Now.AddDays(1) }
            );
    }

    public bool IsTokenBlackListed(string sid)
    {
        var blackListedSid = _redisCache.Get(sid);
        return blackListedSid != null;
    }

    private Domain.Entity.User GetUser(LoginRequest request)
    {
        var username = Username.Create(request.Username);
        var password = Password.Create(request.Password);
        var user = _userRepository.GetByUsernameAndPassword(username, password);

        if (user == null)
            throw new Exception("invalid username or password");

        return user;
    }

    private SigningCredentials GetCredentials()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        securityKey.KeyId = Guid.NewGuid().ToString();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        return credentials;
    }

    private List<Claim> GetClaims(Domain.Entity.User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)),
            new Claim(ClaimTypes.Sid,Guid.NewGuid().ToString())
        };

        if (_userRepository.GetById(user.Id).IsAdmin == true)
            claims.Add(new Claim(ClaimTypes.Role, "admin"));

        return claims;
    }

    private string GenerateToken(List<Claim> claims, SigningCredentials credentials)
    {
        var token = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(Convert.ToInt32(_configuration["Jwt:TokenLifeTime"])),
            signingCredentials: credentials);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenString;
    }
}
