using AutoMapper;
using Blogifier.Shared;

namespace Blogifier.Profiles;

public class PostItemProfile : Profile
{
  public PostItemProfile()
  {
    CreateMap<Post, PostItemDto>();
  }
}
