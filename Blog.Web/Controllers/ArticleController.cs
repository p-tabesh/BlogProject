using Microsoft.AspNetCore.Mvc;
using Blog.Application.Service.Article;
using Blog.Application.Model.Article;
using Blog.Web.Extention;
using Blog.Application.Mapper.Article;

namespace Blog.Web.Controllers;

[ApiController]
[Route("api/articles")]
public class ArticleController : BaseController
{
    private readonly IArticleService _articleService;
    private ILogger<ArticleController> _logger;

    public ArticleController(IArticleService articleService, ILogger<ArticleController> logger)
    {
        _articleService = articleService;
        _logger = logger;   
    }

    [HttpGet]
    [Route("test")]
    public IActionResult TestLog(string message)
    {
        _logger.LogInformation(message);    
        return Ok(message);
    }

    [HttpGet]
    public IActionResult GetArticles()
    {
        var models = ArticleMapper.MapToModel(_articleService.GetArticles());
        return Ok(models);
    }

    [HttpPost]
    public IActionResult CreateArticle(CreateArticleRequest request)
    {
        var id = _articleService.CreateArticle(request, RequestUserId);
        return CreatedAtAction(nameof(CreateArticle), new { Id = id });
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetArticle(int id)
    {
        var article = ArticleMapper.MapToModel(_articleService.GetArticle(id));
        return Ok(article);
    }

    [HttpPost]
    [Route("{id}/like")]
    public IActionResult LikeArticle(int id)
    {
        _articleService.LikeArticle(id, RequestUserId);
        return Ok();
    }

    [HttpPost]
    [Route("{id}/dislike")]
    public IActionResult DislikeArticle(int id)
    {
        _articleService.DislikeArticle(id, RequestUserId);
        return Ok();
    }
}
