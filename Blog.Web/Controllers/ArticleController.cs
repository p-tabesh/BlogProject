using Microsoft.AspNetCore.Mvc;
using Blog.Application.Service.Article;
using Blog.Application.Model.Article;
using Blog.Web.Extention;
using Microsoft.AspNetCore.Authorization;
using Blog.Web.Refit;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Blog.Web.Controllers;

[ApiController]
[Route("/api/articles")]
public class ArticleController : BaseController
{
    private readonly IArticleService _articleService;
    private readonly IRefitServiceTest _refitClient;

    public ArticleController(IArticleService articleService, IRefitServiceTest refitClient)
    {
        _articleService = articleService;
        _refitClient = refitClient;
    }

    [HttpGet]
    public IActionResult GetArticles([FromQuery] string? search)
    {
        if (search != null)
            return Ok(_articleService.GetByTextSearch(search));

        var articles = _articleService.GetArticles();
        return Ok(articles);
    }

    [HttpGet]
    [Route("popular")]
    public IActionResult GetPopularArticles()
    {
        var articles = _articleService.GetPopularArticles();
        return Ok(articles);
    }

    [HttpGet]
    [Route("recents")]
    public IActionResult GetRecentArticles()
    {
        var articles = _articleService.GetRecentArticles();
        return Ok(articles);
    }

    [HttpGet]
    [Route("suggested")]
    public async Task<IActionResult> GetSuggestedArticles()
    {
        var test = await _refitClient.GetTest();
        return Ok(test);
    }

    [HttpGet]
    [Route("mostViewed")]
    public IActionResult GetMostViewedArticles()
    {
        var articles = _articleService.GetMostViewedArticles();
        return Ok(articles);
    }

    [HttpGet]
    [Route("filter")]
    public IActionResult GetWithFilter([FromQuery] ArticleFilterModel filterModel)
    {
        var articles = _articleService.GetWithFilter(filterModel);
        return Ok(articles);
    }

    [HttpPost]
    [Authorize]
    public IActionResult CreateArticle(CreateArticleRequest request)
    {
        var id = _articleService.CreateArticle(request, RequestUserId);
        return CreatedAtAction(nameof(CreateArticle), id);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetArticle(int id)
    {

        var article = await _articleService.GetArticleById(id, HttpContext.Connection.Id);
        return Ok(article);
    }

    [HttpPost]
    [Route("{id}/like")]
    [Authorize]
    public IActionResult LikeArticle(int id)
    {
        _articleService.LikeArticle(id, RequestUserId);
        return Ok();
    }

    [HttpPost]
    [Route("{id}/dislike")]
    [Authorize]
    public IActionResult DislikeArticle(int id)
    {
        _articleService.DislikeArticle(id, RequestUserId);
        return Ok();
    }
}
