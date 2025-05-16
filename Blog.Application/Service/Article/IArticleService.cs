using Blog.Application.Model.Article;

namespace Blog.Application.Service.Article;

public interface IArticleService
{
    public IEnumerable<ArticleViewModel> GetArticles();
    public Task<ArticleViewModel> GetById(int id, string connectionId);
    public int CreateArticle(CreateArticleRequest request, int requestUserId);
    public void EditArticle(UpdateArticleRequest request, int requestUserId);
    public void LikeArticle(int articleId, int requestUserId);
    public void DislikeArticle(int articleId, int requestUserId);
    public IEnumerable<ArticleViewModel> GetRecentArticles();
    public IEnumerable<ArticleViewModel> GetPopularArticles();
    public IEnumerable<ArticleViewModel> GetByTextSearch(string search);
    public IEnumerable<ArticleViewModel> GetWithFilter(ArticleFilterModel filter);
    public IEnumerable<ArticleViewModel> GetMostViewedArticles();
}
