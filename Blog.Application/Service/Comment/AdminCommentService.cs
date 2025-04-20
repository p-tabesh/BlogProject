using AutoMapper;
using Blog.Application.Model.Comment;
using Blog.Domain.IRepository;
using Blog.Domain.IUnitOfWork;

namespace Blog.Application.Service.Comment;

public class AdminCommentService : IAdminCommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AdminCommentService(ICommentRepository commentRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _commentRepository = commentRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public void DisableShow(int commentId)
    {
        var comment = _commentRepository.GetById(commentId);
        comment.DisableShow();
    }

    public IEnumerable<CommentViewModel> GetAll()
    {
        var comments = _commentRepository.GetAll();
        var models = _mapper.Map<List<CommentViewModel>>(comments);   
        return models;
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
