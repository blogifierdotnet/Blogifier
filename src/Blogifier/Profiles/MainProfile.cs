using AutoMapper;
using Blogifier.Blogs;
using Blogifier.Shared;

namespace Blogifier.Profiles;

public class MainProfile : Profile
{
  public MainProfile()
  {
    CreateMap<MainData, MainDto>();
  }
}
