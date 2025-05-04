using Microsoft.AspNetCore.Mvc;
using Blog.Application.Service.Article;
using Blog.Application.Model.Article;
using Blog.Web.Extention;
using Microsoft.AspNetCore.Authorization;
using Confluent.Kafka;

namespace Blog.Web.Controllers;

[ApiController]
[Route("api/articles")]
public class ArticleController : BaseController
{
    private readonly IArticleService _articleService;
    private readonly IProducer<string, string> _producer;
    const string Topic = "articleView-event";

    public ArticleController(IArticleService articleService, IProducer<string, string> producer)
    {
        _articleService = articleService;
        _producer = producer;
    }

    [HttpGet]
    public IActionResult GetArticles()
    {
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
    public IActionResult GetSuggestedArticles()
    {

        return Ok();
    }

    [HttpGet]
    [Route("search")]
    public IActionResult GetArticleByTextSearch([FromQuery] string search)
    {
        var articles = _articleService.GetByTextSearch(search);
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
        return CreatedAtAction(nameof(CreateArticle), new { Id = id });
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetArticle(int id)
    {
        var article = _articleService.GetArticleById(id);
        await _producer.ProduceAsync(Topic, new Message<string, string> { Key = "Article-Views", Value = $"article id {id} viewd" });
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
