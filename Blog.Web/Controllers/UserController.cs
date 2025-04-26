using Blog.Application.Model.User;
using Blog.Application.Service.User;
using Blog.Web.Extention;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers;

[ApiController]
[Route("user")]
public class UserController : BaseController
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }


    [AllowAnonymous]
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
        _userService.ChangeUsername(request, RequestUserId);
        return Ok();
    }

    [HttpPut]
    [Route("change-password")]
    public IActionResult ChangePassword(ChangePasswordRequest request)
    {
        _userService.ChangePassword(request, RequestUserId);
        return Ok();
    }

    [HttpPost]
    [Route("profile/create")]
    public IActionResult CreateProfile(CreateProfileRequest request)
    {
        _userService.CreateProfile(request, RequestUserId);
        return Ok();
    }

    [HttpPut]
    [Route("profile/edit")]
    public IActionResult EditProfile(EditProfileRequest request)
    {
        _userService.EditProfile(request, RequestUserId);
        return Ok();
    }

    [HttpPut]
    [Route("profile/change-image")]
    public IActionResult ChangeProfileImage(ChangeProfileImageLinkRequest request)
    {
        _userService.ChangeProfileImageLink(request, RequestUserId);
        return Ok();
    }

    [HttpGet]
    [Route("{id}/profile")]
    public IActionResult GetProfileByUserId(int id)
    {
        var profile = _userService.GetUserProfile(id);
        return Ok(profile);
    }

    [HttpGet]
    [Route("profile")]
    [Authorize]
    public IActionResult GetCurrentUserProfile()
    {
        var profile = _userService.GetUserProfile(RequestUserId);
        return Ok(profile);
    }
}
