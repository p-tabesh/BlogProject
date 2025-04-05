using Blog.Application.Model.Account;
using Blog.Application.Service.Account;
using Blog.Web.Extention;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers;

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
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = _accountService.GetUser(request);
        var tokenString = _accountService.GenerateToken(user.Id);
        return Ok(new { Token = tokenString });
    }
}
