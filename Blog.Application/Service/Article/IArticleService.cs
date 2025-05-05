using Blog.Application.Model.Article;

namespace Blog.Application.Service.Article;

public interface IArticleService
{
    public IEnumerable<ArticleViewModel> GetArticles();
    public Task<ArticleViewModel> GetArticleById(int id);
    public int CreateArticle(CreateArticleRequest request, int requestUserId);
    public void EditArticle(UpdateArticleRequest request, int requestUserId);
    public void LikeArticle(int articleId, int requestUserId);
    public void DislikeArticle(int articleId, int requestUserId);
    public ArticleViewModel GetRecentArticles();
    public ArticleViewModel GetPopularArticles();
    public ArticleViewModel GetByTextSearch(string search);
    public ArticleViewModel GetWithFilter(ArticleFilterModel filter);
}
