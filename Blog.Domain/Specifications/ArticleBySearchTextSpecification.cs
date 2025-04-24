using Blog.Domain.Entity;
using Core.Repository.Model.Specifications;
using System.Linq.Expressions;

namespace Blog.Domain.Specifications;

public class ArticleBySearchTextSpecification : Specification<Article>
{
    private string _searchText;
    public ArticleBySearchTextSpecification(string searchTest)
    {
        _searchText = searchTest;
    }

    protected override bool IsApplicable => !string.IsNullOrWhiteSpace(_searchText);

    public override Expression<Func<Article, bool>> Expression => article => article.Title.Contains(_searchText) || article.Header.Contains(_searchText);
}
