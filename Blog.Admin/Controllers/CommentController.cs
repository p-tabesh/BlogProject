using Blog.Application.Service.Comment;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Admin.Controllers;

[ApiController]
[Route("comments")]
public class CommentController : Controller
{
    private readonly IAdminCommentService _adminCommentService;

    public CommentController(IAdminCommentService adminCommentService)
    {
        _adminCommentService = adminCommentService;
    }

    [HttpGet]
    public IActionResult GetComments()
    {
        var comments =_adminCommentService.GetAll();
        return Ok(comments);
    }

    [HttpPut]
    [Route("{id}/show")]
    public IActionResult ShowComment(int id)
    {
        _adminCommentService.ShowComment(id);
        return Ok();
    }

    [HttpPut]
    [Route("{id}/reject")]
    public IActionResult RejectComment(int id)
    {
        _adminCommentService.RejectComment(id);
        return Ok();
    }

    [HttpPut]
    [Route("{id}/disable-show")]
    public IActionResult DisableShow(int id)
    {
        _adminCommentService.DisableShow(id);
        return Ok();
    }
}
