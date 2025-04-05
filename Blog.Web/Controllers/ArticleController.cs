using Microsoft.AspNetCore.Mvc;
using Blog.Application.Service.Article;
using Blog.Application.Model.Article;
using Blog.Web.Extention;
using AutoMapper;
using Blog.Application.Mapper.Article;
using Blog.Domain.Entity;

namespace Blog.Web.Controllers;

[ApiController]
[Route("api/Articles")]
public class ArticleController : BaseController
{
    private readonly IArticleService _articleService;

    public ArticleController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    [HttpGet]
    public IActionResult GetArticles()
    {
        //var models = ArticleMapper.MapToModel(_articleService.GetArticles());
        return Ok(/*models*/);
    }

    [HttpPost]
    public IActionResult CreateArticle(CreateArticleRequest request)
    {
        var id = _articleService.CreateArticle(request, RequestUserId);
        return CreatedAtAction(nameof(CreateArticle), new { Id = id });
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetArticle([FromRoute] int id)
    {
        var article = ArticleMapper.MapToModel<Domain.Entity.Article, ArticleViewModel>(_articleService.GetArticle(id));
        return Ok(article);
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
