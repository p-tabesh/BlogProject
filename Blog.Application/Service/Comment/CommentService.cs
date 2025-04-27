using AutoMapper;
using Blog.Application.Model.Category;
using Blog.Application.Model.Comment;
using Blog.Domain.IRepository;
using Blog.Domain.IUnitOfWork;
using Microsoft.Extensions.Logging;

namespace Blog.Application.Service.Comment;

public class CommentService : BaseService<CommentService>, ICommentService
{
    private readonly ICommentRepository _commentRepository;
    public CommentService(IUnitOfWork unitOfWork, ICommentRepository commentRepository, IMapper mapper, ILogger<CommentService> logger)
        : base(unitOfWork, mapper, logger)
    {

        _commentRepository = commentRepository;
    }

    public int AddComment(AddCommentRequest request, int requestUserId)
    {
        var comment = CommentMapper.MapToEntity(request, requestUserId);
        _commentRepository.Add(comment);
        UnitOfWork.Commit();

        return comment.Id;
    }

    public void DislikeCommet(int commentId, int userId)
    {
        var comment = _commentRepository.GetById(commentId);
        comment.Like(userId);
        _commentRepository.Update(comment);
        UnitOfWork.Commit();
    }

    public IEnumerable<CommentViewModel> GetCommentsByArticleId(int articleId)
    {
        var comments = _commentRepository.GetByArticleId(articleId).Where(c => c.RelatedCommentId == null).ToList();
        List<CommentViewModel> models = new();
        foreach (var comment in comments)
        {
            models.Add(CommentMapper.MapFromEntity(comment));
        }

        return models;
    }

    public void LikeComment(int commentId, int userId)
    {
        var comment = _commentRepository.GetById(commentId);
        comment.Like(userId);
        _commentRepository.Update(comment);
        UnitOfWork.Commit();
    }

    public void ReplyComment(ReplyCommentRequest request, int requestUserId)
    {

        var comment = _commentRepository.GetById(request.RelatedComment);
        comment.ReplyComment(request.Text, requestUserId);

        _commentRepository.Update(comment);
        UnitOfWork.Commit();
    }
}
