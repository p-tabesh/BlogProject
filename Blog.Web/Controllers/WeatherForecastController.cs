using Microsoft.AspNetCore.Mvc;
using Blog.Infrastructure.Context;
using Blog.Domain.Entity;
namespace Blog.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly BlogDbContext _dbContext;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, BlogDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet]
    [Route("Test")]
    public IActionResult Get()
    {
        var article = _dbContext.Article.FirstOrDefault();
        article.Like(1);
        _dbContext.Update(article);
        _dbContext.SaveChanges();
        return Ok(article.Id);
    }
}
