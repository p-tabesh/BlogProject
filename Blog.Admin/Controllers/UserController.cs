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

    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _adminUserService.GetAll();
        return Ok(users);
    }

    [HttpPost]
    public IActionResult CreateAdminUser(RegisterRequest request)
    {
        var reply = _adminUserService.CreateAdminUser(request);
        return CreatedAtAction(nameof(CreateAdminUser), new { Id = reply });

    }

    [HttpPut]
    [Route("{id}/active")]
    public IActionResult ActiveUser(int id)
    {
        _adminUserService.ActiveUser(id);
        return Ok();
    }

    [HttpPut]
    [Route("{id}/deActive")]
    public IActionResult DeActiveUser(int id)
    {
        _adminUserService.DeActiveUser(id);
        return Ok();
    }
}
