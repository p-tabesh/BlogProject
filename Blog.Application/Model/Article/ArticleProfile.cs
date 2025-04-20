using Blog.Domain.Entity;
using ProfileMapper = AutoMapper.Profile;

namespace Blog.Application.Model.Article;

public class ArticleProfile : ProfileMapper
{
    public ArticleProfile()
    {
        CreateMap<Domain.Entity.Article,ArticleViewModel>();
        CreateMap<ArticleViewModel, Domain.Entity.Article>();
    }
}
