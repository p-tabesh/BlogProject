using AutoMapper;
using Blog.Application.Model.Article;
using Blog.Domain.Entity;
using Blog.Domain.IRepository;
using Blog.Domain.IUnitOfWork;
using Blog.Domain.Specifications;

namespace Blog.Application.Service.Article;

public class ArticleService : IArticleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;
    private readonly AI _ai;

    public ArticleService(IUnitOfWork unitOfWork, IArticleRepository articleRepository, IMapper mapper, AI ai)
    {
        _unitOfWork = unitOfWork;
        _articleRepository = articleRepository;
        _mapper = mapper;
        _ai = ai;
    }

    public IEnumerable<ArticleViewModel> GetArticles()
    {
        var articles = _articleRepository.GetWithSpecifications(new PublishedArticleSpecification());
        var models = _mapper.Map<List<ArticleViewModel>>(articles);
        return models;
    }

    public async Task<ArticleViewModel> GetArticleAsync(int id)
    {
        //var model = _mapper.Map<ArticleViewModel>(_articleRepository.GetById(id));

        var article = _articleRepository.GetById(id);
        var articleSummary = await _ai.ChatAsync($"Summerize this: {article.Text}");

        var model = new ArticleViewModel(article.Id, article.Header, article.Title, article.Text, article.Tags, article.PublishDate, article.Status.ToString(), article.Likes,
            article.Dislikes, article.AuthorUserId, article.CategoryId, article.PreviewImageLink, articleSummary);
        return model;
    }

    public IEnumerable<Domain.Entity.Article> GetArticlesForAdmin() => _articleRepository.GetAll();

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
        _unitOfWork.Commit();

        return article.Id;
    }

    public void UpdateArticle(UpdateArticleRequest request, int requestUserId)
    {
        var article = _articleRepository.GetById(request.ArticleId);

        if (article.AuthorUserId != requestUserId)
            throw new Exception("Article doesn't belong to user");

        article.UpdateArticle(request.NewHeader, request.NewTitle, request.NewText, request.NewPreviewImageLink);

        _articleRepository.Update(article);
        _unitOfWork.Commit();
    }

    public void AcceptArticle(int articleId)
    {
        var article = _articleRepository.GetById(articleId);
        article.Accept();

        _articleRepository.Update(article);
        _unitOfWork.Commit();
    }

    public void RejectArticle(int articleId)
    {
        var article = _articleRepository.GetById(articleId);
        article.Reject();

        _articleRepository.Update(article);
        _unitOfWork.Commit();
    }

    public void LikeArticle(int articleId, int requestUserId)
    {
        var article = _articleRepository.GetById(articleId);
        article.Like(requestUserId);

        _articleRepository.Update(article);
        _unitOfWork.Commit();
    }

    public void DislikeArticle(int articleId, int requestUserId)
    {
        var article = _articleRepository.GetById(articleId);
        article.Dislike(requestUserId);

        _articleRepository.Update(article);
        _unitOfWork.Commit();
    }
}


