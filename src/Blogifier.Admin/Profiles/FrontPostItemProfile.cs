using AutoMapper;
using Blogifier.Shared;

namespace Blogifier.Admin.Profiles;

public class FrontPostItemProfile : Profile
{
  public FrontPostItemProfile()
  {
    CreateMap<PostItemDto, FrontPostItemDto>();
  }
}
