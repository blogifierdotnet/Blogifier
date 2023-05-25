using AutoMapper;
using Blogifier.Blogs;
using Blogifier.Shared;

namespace Blogifier.Profiles;

public class BlogProfile : Profile
{
  public BlogProfile()
  {
    CreateMap<BlogData, BaseModel>();
  }
}
