using Blog.Application.Model.Profile;
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
    [Route("register")]
    public IActionResult RegisterUser(RegisterRequest request)
    {
        var reply = _userService.RegisterUser(request);
        return Ok(reply);
    }

    [HttpPut]
    [Route("change-username")]
    public IActionResult ChangeUsername(ChangeUsernameRequest request)
    {
        _userService.ChangeUsername(request);
        return Ok();
    }

    [HttpPut]
    [Route("change-password")]
    public IActionResult ChangePassword(ChangePasswordRequest request)
    {
        _userService.ChangePassword(request);
        return Ok();
    }

    [HttpPut]
    [Route("create-profile")]
    public IActionResult CreateProfile(CreateProfileRequest request)
    {
        return Ok();
    }
}
