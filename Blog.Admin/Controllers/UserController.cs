using Blog.Application.Model.User;
using Blog.Application.Service.User;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Admin.Controllers;

[ApiController]
[Route("user")]
public class UserController : Controller
{
    private IAdminUserService _adminUserService;


    public UserController(IAdminUserService adminUserService)
    {
        _adminUserService = adminUserService;
    }

    [HttpPost]
    public IActionResult CreateAdminUser(RegisterRequest request)
    {
        var reply = _adminUserService.CreateAdminUser(request);
        return CreatedAtAction(nameof(CreateAdminUser), new { Id = reply });

    }
}
