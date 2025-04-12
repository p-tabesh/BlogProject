using Blog.Application.Model.User;
using Blog.Application.Service.User;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers;

[ApiController]
[Route("user")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public IActionResult RegisterUser(RegisterRequest request)
    {
        var reply = _userService.RegisterUser(request);
        return Ok(reply);
    }

    [HttpPut]
    [Route("changeusername")]
    public IActionResult ChangeUsername(ChangeUsernameRequest request)
    {
        _userService.ChangeUsername(request);
        return Ok();
    }
    
}
