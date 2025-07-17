using Blog.Application.Model.Account;
using Blog.Application.Service.Account;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Blog.Admin.Extention;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Admin.Controllers;

[ApiController]
[Route("account")]
public class AccountController : BaseController
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public IActionResult Login(LoginRequest request)
    {
        var token = _accountService.GetLoginToken(request);
        return Ok(new { Token = token });
    }

    [HttpGet]
    [Route("logout")]
    public IActionResult Logout()
    {
        _accountService.Logout(new JwtSecurityTokenHandler().ReadJwtToken(AuthorizationValue));
        return Ok();
    }
}
