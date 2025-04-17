using Blog.Application.Service.Article;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Admin.Controllers;

[ApiController]
[Route("[controller]")]
public class ArticleController : ControllerBase
{
    private readonly ILogger<ArticleController> _logger;
    private readonly IAdminArticleService _adminArticleService;

    public ArticleController(ILogger<ArticleController> logger, IAdminArticleService adminArticleService)
    {
        _adminArticleService = adminArticleService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetArticles()
    {
        var articles = _adminArticleService.GetArticles();
        return Ok(articles);
    }
}
