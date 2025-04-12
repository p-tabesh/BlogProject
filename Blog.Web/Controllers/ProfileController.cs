using Blog.Application.Model.Profile;
using Blog.Application.Service.Profile;
using Blog.Web.Extention;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers;

[ApiController]
[Route("profile")]
public class ProfileController : BaseController
{
    private readonly IProfileService _profileService;
    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpPost]
    public IActionResult CreateProfile(CreateProfileRequest request)
    {
        var id = _profileService.CreateProfile(request, RequestUserId);
        return CreatedAtAction(nameof(CreateProfile), new { Id = id });
    }

    [HttpGet]
    public IActionResult GetProfile()
    {
        var profile = _profileService.GetProfileByUserId(RequestUserId);
        return Ok(profile);
    }

    [HttpPut]
    [Route("editProfile")]
    public IActionResult EditProfile(EditProfileRequest request)
    {
        _profileService.EditProfile(request, RequestUserId);
        return Ok();
    }

    [HttpPut]
    [Route("editimage")]
    public IActionResult ChangeProfile(ChangeProfileImageLinkRequest request)
    {
        _profileService.ChangeProfileImageLink(request, RequestUserId);
        return Ok();
    }
}
