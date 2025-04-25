using AutoMapper;
using Blog.Application.Model.Article;
using Blog.Domain.IRepository;
using Blog.Domain.IUnitOfWork;
using Blog.Domain.Specifications;
using Microsoft.Extensions.Logging;

namespace Blog.Application.Service.Article;

public class ArticleService : BaseService<ArticleService>, IArticleService
{
    private readonly IArticleRepository _articleRepository;

    public ArticleService(IUnitOfWork unitOfWork, IArticleRepository articleRepository, IMapper mapper, ILogger<ArticleService> logger)
        : base(unitOfWork, mapper, logger)
    {
        _articleRepository = articleRepository;
    }

    public IEnumerable<ArticleViewModel> GetArticles()
    {
        var articles = _articleRepository.GetWithSpecifications(new PublishedArticleSpecification());
        var models = Mapper.Map<List<ArticleViewModel>>(articles);
        return models;
    }

    public ArticleViewModel GetArticleById(int id)
    {
        var model = Mapper.Map<ArticleViewModel>(_articleRepository.GetById(id));
        return model;
    }

    public int CreateArticle(CreateArticleRequest request, int requestUserId)
    {
        var article = new Domain.Entity.Article(request.Header,
            request.Title,
            request.Text,
            request.Tags,
            request.PreviewImageLink,
            request.PublishDate,
            requestUserId,
            request.categoryId);

        _articleRepository.Add(article);
        UnitOfWork.Commit();

        return article.Id;
    }

    public void EditArticle(UpdateArticleRequest request, int requestUserId)
    {
        var article = _articleRepository.GetById(request.ArticleId);

        if (article.AuthorUserId != requestUserId)
            throw new Exception("Article doesn't belong to user");

        article.Edit(request.NewHeader, request.NewTitle, request.NewText, request.NewPreviewImageLink);

        _articleRepository.Update(article);
        UnitOfWork.Commit();
    }

    public void LikeArticle(int articleId, int requestUserId)
    {
        var article = _articleRepository.GetById(articleId);
        article.Like(requestUserId);

        _articleRepository.Update(article);
        UnitOfWork.Commit();
    }

    public void DislikeArticle(int articleId, int requestUserId)
    {
        var article = _articleRepository.GetById(articleId);
        article.Dislike(requestUserId);

        _articleRepository.Update(article);
        UnitOfWork.Commit();
    }
}


