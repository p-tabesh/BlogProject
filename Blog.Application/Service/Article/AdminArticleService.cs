using AutoMapper;
using Blog.Application.Model.Article;
using Blog.Domain.IRepository;
using Blog.Domain.IUnitOfWork;
using Microsoft.Extensions.Logging;

namespace Blog.Application.Service.Article;

public class AdminArticleService : BaseService<AdminArticleService>, IAdminArticleService
{
    private readonly IArticleRepository _articleRepository;

    public AdminArticleService(IUnitOfWork unitOfWork, IArticleRepository articleRepository, IMapper mapper, ILogger<AdminArticleService> logger)
        : base(unitOfWork, mapper, logger)
    {
        _articleRepository = articleRepository;
    }

    public void Accept(int articleId)
    {
        var article = _articleRepository.GetById(articleId);
        article.Accept();
    }

    public IEnumerable<ArticleViewModel> GetArticles()
    {
        var articles = _articleRepository.GetAll();
        var models = Mapper.Map<List<ArticleViewModel>>(articles);
        return models;
    }

    public void Reject(int articleId)
    {
        var article = _articleRepository.GetById(articleId);
        article.Reject();
    }
}
