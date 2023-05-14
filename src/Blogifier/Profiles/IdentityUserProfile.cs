using AutoMapper;
using Blogifier.Models;
using Microsoft.AspNetCore.Identity;

namespace Blogifier.Profiles;

public class IdentityUserProfile : Profile
{
  public IdentityUserProfile()
  {
    CreateMap<IdentityUser, IdentityUserDto>();
  }
}
