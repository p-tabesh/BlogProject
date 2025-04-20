using AutoMapper;
using Blog.Application.Model.Article;
using Blog.Domain.IRepository;
using Blog.Domain.IUnitOfWork;

namespace Blog.Application.Service.Article;

public class AdminArticleService : IAdminArticleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;   

    public AdminArticleService(IUnitOfWork unitOfWork, IArticleRepository articleRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _articleRepository = articleRepository; 
        _mapper = mapper;
    }

    public void Accept(int articleId)
    {
        var article = _articleRepository.GetById(articleId);
        article.Accept();
    }

    public IEnumerable<ArticleViewModel> GetArticles()
    {
        var articles = _articleRepository.GetAll();
        var models = _mapper.Map<List<ArticleViewModel>>(articles);
        return models;
    }

    public void Reject(int articleId)
    {
        var article = _articleRepository.GetById(articleId);
        article.Reject();
    }
}
