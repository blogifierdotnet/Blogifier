using AutoMapper;
using Blogifier.Blogs;
using Blogifier.Shared;

namespace Blogifier.Profiles;

public class PostSlugProfile : Profile
{
  public PostSlugProfile()
  {
    CreateMap<PostSlug, PostSlugDto>();
  }
}
