using Blog.Application.Model.Comment;
using Blog.Domain.IRepository;
using Blog.Domain.IUnitOfWork;

namespace Blog.Application.Service.Comment;

public class AdminCommentService : IAdminCommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AdminCommentService(ICommentRepository commentRepository, IUnitOfWork unitOfWork)
    {
        _commentRepository = commentRepository;
        _unitOfWork = unitOfWork;
    }

    public void DisableShow(int commentId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CommentViewModel> GetAll()
    {
        throw new NotImplementedException();
    }

    public void RejectComment(int commentId)
    {
        throw new NotImplementedException();
    }

    public void ShowComment(int commentId)
    {
        throw new NotImplementedException();
    }
}
