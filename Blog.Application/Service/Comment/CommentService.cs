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
        var comment = new Domain.Entity.Comment(request.Text,request.ArticleId,request.RelatedCommentId,requestUserId);
        _commentRepository.Add(comment);
        _unitOfWork.Commit();

        return comment.Id;
    }
}
