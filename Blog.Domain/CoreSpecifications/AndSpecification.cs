using Blog.Domain.Entity;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace Core.Repository.Model.Specifications;

public class AndSpecification<T> : Specification<T>
{
    private readonly Specification<T> _left;
    private readonly Specification<T> _right;

    public AndSpecification(Specification<T> left, Specification<T> right)
    {
        _right = right;
        _left = left;
    }

    public override Expression<Func<T, bool>> Expression => _left.Expression.And(_right.Expression);
}

public class ArticleSpecification : Specification<Article>
{
    Article _article;
    Expression<Func<Article, bool>> _exp;
    public ArticleSpecification(Article article, Expression<Func<Article, bool>> expression)
    {
        _article = article;
        _exp = expression;
    }

    public override Expression<Func<Article, bool>> Expression => _exp;
}

public class test
{
    public void salam()
    {
        var article = new Article();
        var spec = new ArticleSpecification(article, a=> a.Id == 123);
        
    }

}