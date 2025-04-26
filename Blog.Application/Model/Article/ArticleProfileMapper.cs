using Blog.Domain.Entity;
using System.Runtime.CompilerServices;
using ProfileMapper = AutoMapper.Profile;

namespace Blog.Application.Model.Article;

public class ArticleProfileMapper : ProfileMapper
{
    public ArticleProfileMapper()
    {
        CreateMap<Domain.Entity.Article, ArticleViewModel>();
        CreateMap<ArticleViewModel, Domain.Entity.Article>();
        CreateMap<CreateArticleRequest, Domain.Entity.Article>()
            .ConstructUsing((src, context) =>
        {
            var userId = context.Items.ContainsKey("AuthorUserId")
                ? (int)context.Items["AuthorUserId"]
                : throw new ArgumentException("AuthorUserId is missing from context");

            return new Domain.Entity.Article(src.Header,src.Title,src.Text,src.Tags,src.PreviewImageLink,src.PublishDate, userId,src.CategoryId);
        }); ;
    }
}
