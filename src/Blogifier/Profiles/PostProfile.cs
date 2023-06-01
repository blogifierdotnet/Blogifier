using AutoMapper;
using Blogifier.Shared;

namespace Blogifier.Profiles;

public class PostProfile : Profile
{
  public PostProfile()
  {
    CreateMap<Post, PostDto>();
    CreateMap<Post, PostItemDto>();
    CreateMap<Post, PostToHtmlDto>();
    CreateMap<Post, PostEditorDto>()
      .ForMember(d => d.Categories, opt => opt.MapFrom(src => src.PostCategories))
      .ReverseMap()
      .ForMember(d => d.PostCategories, opt => opt.MapFrom(src => src.Categories));
  }
}
