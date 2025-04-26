using ProfileMapper = AutoMapper.Profile;

namespace Blog.Application.Model.Category;

public class CategoryProfileMapper : ProfileMapper
{
    public CategoryProfileMapper()
    {
        CreateMap<Domain.Entity.Category, CategoryViewModel>();
        CreateMap<CategoryViewModel, Domain.Entity.Category>();
    }
}
