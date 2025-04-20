using Blog.Application.Model.Article;

namespace Blog.Application.Service.Article;

public interface IAdminArticleService
{
    public IEnumerable<ArticleViewModel> GetArticles();
    public void Reject(int articleId);
    public void Accept(int articleId);
}
