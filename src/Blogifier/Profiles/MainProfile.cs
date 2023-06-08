using AutoMapper;
using Blogifier.Blogs;
using Blogifier.Models;

namespace Blogifier.Profiles;

public class MainProfile: Profile
{
  public MainProfile()
  {
    CreateMap<BlogData, MainModel>();
  }
}
