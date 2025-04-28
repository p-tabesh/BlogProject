using Blog.Domain.Entity;
using Core.Repository.Model.Specifications;
using System.Linq.Expressions;

namespace Blog.Domain.Specifications;

public class ArticleCategorySpecification : Specification<Article>
{
    private int? _categoryId;

    public ArticleCategorySpecification(int? categoryId)
    {
        _categoryId = categoryId;
    }

    protected override bool IsApplicable => _categoryId.HasValue;
    public override Expression<Func<Article, bool>> Expression => article => article.CategoryId == _categoryId;
}
