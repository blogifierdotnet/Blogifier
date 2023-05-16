using AutoMapper;
using Blogifier.Blogs;
using Blogifier.Models;
using Blogifier.Shared;

namespace Blogifier.Profiles;

public class BlogSumProfile : Profile
{
  public BlogSumProfile()
  {
    CreateMap<BlogSumInfo, BlogSumDto>();
  }
}
