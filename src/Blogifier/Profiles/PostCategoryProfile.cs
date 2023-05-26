using AutoMapper;
using Blogifier.Shared;

namespace Blogifier.Profiles;

public class PostCategoryProfile : Profile
{
  public PostCategoryProfile()
  {
    CreateMap<PostCategory, CategoryDto>()
      .IncludeMembers(m => m.Category)
      .ReverseMap();
  }
}
