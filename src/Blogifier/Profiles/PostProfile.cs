using AutoMapper;
using Blogifier.Shared;

namespace Blogifier.Profiles;

public class PostProfile : Profile
{
  public PostProfile()
  {
    CreateMap<Post, PostDto>();
    CreateMap<Post, PostBriefDto>();
    CreateMap<Post, PostItemDto>();
    CreateMap<Post, PostToHtmlDto>();
    CreateMap<Post, PostEditorDto>()
      .ForMember(d => d.Categories, opt => opt.MapFrom(src => src.PostCategories))
      //.ForMember(d => d.Storages, opt => opt.MapFrom(src => src.StorageReferences))
      .ReverseMap()
      .ForMember(d => d.PostCategories, opt => opt.MapFrom(src => src.Categories))
      //.ForMember(d => d.StorageReferences, opt => opt.MapFrom(src => src.Storages))
      ;
  }
}
