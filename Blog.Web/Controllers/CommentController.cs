using Blog.Web.Extention;
using Microsoft.AspNetCore.Mvc;
using Blog.Application.Service.Comment;
using Blog.Application.Model.Comment;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Web.Controllers;

[ApiController]
[Route("api/comments")]
public class CommentController : BaseController
{
    private ICommentService _commentSerivce;

    public CommentController(ICommentService commentService)
    {
        _commentSerivce = commentService;
    }

    [HttpGet]
    public IActionResult GetCommentsByArticleId(int articleId)
    {
        var comments = _commentSerivce.GetCommentsByArticleId(articleId);
        return Ok(comments);
    }

    [HttpPost]
    [Authorize]
    public IActionResult AddComment(AddCommentRequest request)
    {
        var id = _commentSerivce.AddComment(request,RequestUserId);
        return CreatedAtAction(nameof(AddComment), new { Id = id });
    }

    [HttpPost]
    [Route("reply")]
    [Authorize]
    public IActionResult ReplyComment(ReplyCommentRequest request)
    {
        _commentSerivce.ReplyComment(request, RequestUserId);
        return Ok();
    }
}
