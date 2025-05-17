using AutoMapper;
using Blog.Application.Model.Article;
using Blog.Domain.Event;
using Blog.Domain.IRepository;
using Blog.Domain.IUnitOfWork;
using Blog.Domain.Specifications;
using Microsoft.Extensions.Logging;

namespace Blog.Application.Service.Article;

public class ArticleService : BaseService<ArticleService>, IArticleService
{
    private readonly IArticleRepository _articleRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IEventProducer _producer;

    public ArticleService(IUnitOfWork unitOfWork, IArticleRepository articleRepository, ICategoryRepository categoryRepository,
        IMapper mapper, ILogger<ArticleService> logger, IEventProducer producer)
        : base(unitOfWork, mapper, logger)
    {
        _categoryRepository = categoryRepository;
        _articleRepository = articleRepository;
        _producer = producer;
    }

    public IEnumerable<ArticleViewModel> GetArticles()
    {
        var articles = _articleRepository.GetWithSpecification(new PublishedArticleSpecification());
        var models = Mapper.Map<List<ArticleViewModel>>(articles);
        return models;
    }

    public async Task<ArticleViewModel> GetById(int id, string connectionId)
    {
        var article = await _articleRepository.GetByIdAsync(id);

        if (article == null || article.Status != Domain.Enum.Status.Published)
            throw new Exception("article doesn't exists or not released yet.");

        await _producer.ProduceAsync("articleView-event", new ArticleViewEventModel(connectionId, id, DateTime.Now));

        var model = Mapper.Map<ArticleViewModel>(article);
        return model;
    }

    public int CreateArticle(CreateArticleRequest request, int requestUserId)
    {
        var category = _categoryRepository.GetById(request.CategoryId);
        if (category == null)
            throw new Exception("category doesn't exists");

        var article = Mapper.Map<Domain.Entity.Article>(request, option => option.Items["AuthorUserId"] = requestUserId);
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
        if (article == null)
            throw new Exception("article doesn't exists");

        article.Like(requestUserId);

        _articleRepository.Update(article);
        UnitOfWork.Commit();
    }

    public void DislikeArticle(int articleId, int requestUserId)
    {
        var article = _articleRepository.GetById(articleId);
        if (article == null)
            throw new Exception("article doesn't exists");

        article.Dislike(requestUserId);

        _articleRepository.Update(article);
        UnitOfWork.Commit();
    }

    public IEnumerable<ArticleViewModel> GetRecentArticles()
    {
        var articles = _articleRepository.GetWithSpecification(new RecentArticleSpecification().And(new PublishedArticleSpecification()));
        var models = Mapper.Map<List<ArticleViewModel>>(articles);

        return models;
    }

    public IEnumerable<ArticleViewModel> GetPopularArticles()
    {
        var articles = _articleRepository.GetWithSpecification(new PublishedArticleSpecification()).OrderBy(a => a.Likes.Count);
        var models = Mapper.Map<List<ArticleViewModel>>(articles);

        return models;
    }

    public IEnumerable<ArticleViewModel> GetMostViewedArticles()
    {
        var articles = _articleRepository.GetWithSpecification(new PublishedArticleSpecification()).OrderByDescending(a => a.Views);
        var models = Mapper.Map<List<ArticleViewModel>>(articles);

        return models;
    }

    public IEnumerable<ArticleViewModel> GetByTextSearch(string search)
    {
        var articles = _articleRepository.GetWithSpecification(new ArticleBySearchTextSpecification(search).And(new PublishedArticleSpecification()));
        var models = Mapper.Map<List<ArticleViewModel>>(articles);

        return models;
    }

    public IEnumerable<ArticleViewModel> GetWithFilter(ArticleFilterModel filter)
    {
        var specification = SpecificationBuilder.GetFilterSpecifications(filter);
        var articles = _articleRepository.GetWithSpecification(specification);
        var models = Mapper.Map<List<ArticleViewModel>>(articles);

        return models;
    }
}