using ProfileMapper = AutoMapper.Profile;

namespace Blog.Application.Model.Comment;

public class CommentProfileMapper : ProfileMapper
{
    public CommentProfileMapper()
    {
        CreateMap<Domain.Entity.Comment, CommentViewModel>()
            .ForMember(dest => dest.ChildrenComments,opt => opt.MapFrom(src => src.ChildrenComments ?? new List<Domain.Entity.Comment>()))
            .PreserveReferences();
        CreateMap<CommentViewModel, Domain.Entity.Comment>();
    }
}
