using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers;

[ApiController]
[Route("user")]
public class UserController : Controller
{
    
    public UserController()
    {
        
    }

    public IActionResult Index()
    {
        return View();
    }
}
