using Blog.Application.Model.Article;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Blog.Application.Service.Article;

public interface IArticleService
{
    public IEnumerable<Domain.Entity.Article> GetArticles();
    public Domain.Entity.Article GetArticle(int id);
    public IEnumerable<Domain.Entity.Article> GetArticlesByCategoryId(int categoryId);
    public IEnumerable<Domain.Entity.Article> GetArticlesByTag(string tag);
    public IEnumerable<Domain.Entity.Article> GetArticlesByUserId(int userId);
    public IEnumerable<Domain.Entity.Article> GetArticlesForAdmin();
    public int CreateArticle(CreateArticleRequest request, int requestUserId);
    public void UpdateArticle(UpdateArticleRequest request, int requestUserId);
    public void AcceptArticle(int articleId);
    public void RejectArticle(int articleId);
    public void LikeArticle(int articleId, int requestUserId);
    public void DislikeArticle(int articleId, int requestUserId);
}
