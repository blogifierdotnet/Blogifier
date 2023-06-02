using AutoMapper;
using Blogifier.Shared;

namespace Blogifier.Admin.Profiles;

public class UserProfile : Profile
{
  public UserProfile()
  {
    CreateMap<UserInfoDto, FrontUserInfoDto>().ReverseMap();
    CreateMap<UserInfoDto, UserEditorDto>();
  }
}
