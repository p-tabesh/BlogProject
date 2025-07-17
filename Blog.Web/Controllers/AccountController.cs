using Blog.Application.Model.Account;
using Blog.Application.Service.Account;
using Blog.Web.Extention;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Blog.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : BaseController
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Login(LoginRequest request)
    {
        var token = _accountService.GetLoginToken(request);
        return Ok(new { Token = token });
    }

    [HttpGet]
    [Route("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        _accountService.Logout(new JwtSecurityTokenHandler().ReadJwtToken(AuthorizationValue));
        return Ok();
    }
}
