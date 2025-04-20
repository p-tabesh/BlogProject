using Blog.Application.Model.Article;

namespace Blog.Application.Service.Article;

public interface IArticleService
{
    public IEnumerable<ArticleViewModel> GetArticles();
    public ArticleViewModel GetArticle(int id);
    public int CreateArticle(CreateArticleRequest request, int requestUserId);
    public void UpdateArticle(UpdateArticleRequest request, int requestUserId);
    public void LikeArticle(int articleId, int requestUserId);
    public void DislikeArticle(int articleId, int requestUserId);
}
