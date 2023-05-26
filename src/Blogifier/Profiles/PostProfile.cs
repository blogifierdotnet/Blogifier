using AutoMapper;
using Blogifier.Shared;

namespace Blogifier.Profiles;

public class PostProfile : Profile
{
  public PostProfile()
  {
    CreateMap<Post, PostDto>().ReverseMap();
    CreateMap<Post, PostEditorDto>().ReverseMap();
    CreateMap<Post, PostItemDto>();
  }
}
