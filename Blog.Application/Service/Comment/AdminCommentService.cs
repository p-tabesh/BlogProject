using AutoMapper;
using Blog.Application.Model.Comment;
using Blog.Domain.IRepository;
using Blog.Domain.IUnitOfWork;
using Microsoft.Extensions.Logging;

namespace Blog.Application.Service.Comment;

public class AdminCommentService : BaseService<AdminCommentService>, IAdminCommentService
{
    private readonly ICommentRepository _commentRepository;

    public AdminCommentService(ICommentRepository commentRepository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<AdminCommentService> logger)
        :base(unitOfWork,mapper, logger)
    {
        _commentRepository = commentRepository;
    }

    public IEnumerable<CommentViewModel> GetAllComments()
    {
        var comments = _commentRepository.GetAll();
        List<CommentViewModel> models = new();

        foreach (var comment in comments)
            models.Add(CommentMapper.MapFromEntity(comment));

        return models;
    }

    public void HideComment(int commentId)
    {
        var comment = _commentRepository.GetById(commentId);
        comment.Hide();
        _commentRepository.Update(comment);
        UnitOfWork.Commit();
    }

    public void ShowComment(int commentId)
    {
        var comment = _commentRepository.GetById(commentId);
        comment.Show();
        _commentRepository.Update(comment);
        UnitOfWork.Commit();
    }
}
