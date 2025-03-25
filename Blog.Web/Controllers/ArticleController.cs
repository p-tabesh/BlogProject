using Microsoft.AspNetCore.Mvc;
using Blog.Infrastructure.Context;

namespace Blog.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticleController : ControllerBase
{


    private readonly ILogger<ArticleController> _logger;
    private readonly BlogDbContext _dbContext;

    public ArticleController(ILogger<ArticleController> logger, BlogDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet]
    [Route("dsa")]
    public IActionResult Get()
    { 
        return Ok();
    }
}
