using AutoMapper;
using Blogifier.Models;
using Blogifier.Shared;

namespace Blogifier.Profiles;

public class PostProfile : Profile
{
  public PostProfile()
  {
    CreateMap<Post, PostDto>().ReverseMap();
    CreateMap<Post, PostEditorDto>().ReverseMap();
  }
}
