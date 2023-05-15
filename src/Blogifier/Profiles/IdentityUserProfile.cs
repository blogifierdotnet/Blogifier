using AutoMapper;
using Blogifier.Identity;
using Blogifier.Models;

namespace Blogifier.Profiles;

public class IdentityUserProfile : Profile
{
  public IdentityUserProfile()
  {
    CreateMap<IdentityUser, IdentityUserDto>();
  }
}
