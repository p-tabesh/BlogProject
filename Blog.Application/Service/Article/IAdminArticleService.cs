namespace Blog.Application.Service.Article;

public interface IAdminArticleService
{
    public IEnumerable<Domain.Entity.Article> GetArticles();
    public void Reject();
    public void Accept();
}
