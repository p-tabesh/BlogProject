using Blog.Application.Model.Article;
using Blog.Domain.Entity;
using Blog.Domain.IRepository;
using Blog.Domain.IUnitOfWork;

namespace Blog.Application.Service.Article;

public class ArticleService : IArticleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IArticleRepository _articleRepository;

    public ArticleService(IUnitOfWork unitOfWork, IArticleRepository articleRepository)
    {
        _unitOfWork = unitOfWork;
        _articleRepository = articleRepository;
    }

    public IEnumerable<Domain.Entity.Article> GetArticles() => _articleRepository.GetAll();

    public Domain.Entity.Article GetArticle(int id) => _articleRepository.GetById(id);

    public IEnumerable<Domain.Entity.Article> GetArticlesByCategoryId(int categoryId) => _articleRepository.GetArticlesByCategoryId(categoryId);

    public IEnumerable<Domain.Entity.Article> GetArticlesByTag(string tag) => _articleRepository.GetArticlesWithTag(tag);

    public IEnumerable<Domain.Entity.Article> GetArticlesByUserId(int userId) => _articleRepository.GetArticlesByUserId(userId);

    public IEnumerable<Domain.Entity.Article> GetArticlesForAdmin() => _articleRepository.GetAllForAdmin();

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


