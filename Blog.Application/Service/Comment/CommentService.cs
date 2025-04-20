using Blog.Application.Model.Comment;
using Blog.Domain.IRepository;
using Blog.Domain.IUnitOfWork;

namespace Blog.Application.Service.Comment;

public class CommentService : ICommentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICommentRepository _commentRepository;
    public CommentService(IUnitOfWork unitOfWork, ICommentRepository commentRepository)
    {
        _unitOfWork = unitOfWork;
        _commentRepository = commentRepository;
    }

    public int AddComment(AddCommentRequest request, int requestUserId)
    {
        var comment = new Domain.Entity.Comment(request.Text, request.RelatedCommentId, request.ArticleId, requestUserId);
        _commentRepository.Add(comment);
        _unitOfWork.Commit();

        return comment.Id;
    }

    public void ReplyComment(ReplyCommentRequest request, int requestUserId)
    {
        
        var comment = _commentRepository.GetById(request.RelatedComment);
        comment.ReplyComment(request.Text, requestUserId);

        _commentRepository.Update(comment);
        _unitOfWork.Commit();
    }
}
