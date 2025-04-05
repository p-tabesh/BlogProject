using Microsoft.AspNetCore.Mvc;
using Blog.Infrastructure.Context;
using Blog.Application.Service.Article;
using Blog.Application.Model.Article;
using Blog.Web.Extention;

namespace Blog.Web.Controllers;

[ApiController]
[Route("api/Articles")]
public class ArticleController : BaseController
{
    private readonly IArticleService _articleService;
    private readonly ILogger<ArticleController> _logger;
    private readonly BlogDbContext _dbContext;

    public ArticleController(BlogDbContext dbContext, IArticleService articleService)
    {
        _articleService = articleService;
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult GetArticles() => Ok(_articleService.GetArticles());

    [HttpPost]
    public IActionResult CreateArticle(CreateArticleRequest request)
    {
        var id = _articleService.CreateArticle(request, RequestUserId);
        return Ok(id);
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetArticle([FromRoute] int id)
    {
        return Ok(id);
    }

    [HttpPost]
    [Route("{id}/Like")]
    public IActionResult LikeArticle([FromRoute] int id)
    {
        return Ok(id);
    }

    [HttpPost]
    [Route("{id}/Dislike")]
    public IActionResult DislikeArticle([FromRoute] int id)
    {
        return Ok(id);
    }
}
