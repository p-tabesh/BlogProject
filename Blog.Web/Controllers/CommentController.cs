using Blog.Web.Extention;
using Microsoft.AspNetCore.Mvc;
using Blog.Application.Service.Comment;
using Blog.Application.Model.Comment;

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

    public IActionResult AddComment(AddCommentRequest request)
    {
        var id = _commentSerivce.AddComment(request,RequestUserId);
        return CreatedAtAction(nameof(AddComment), new { Id = id });
    }
}
