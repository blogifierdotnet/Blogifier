using AutoMapper;
using Blogifier.Shared;

namespace Blogifier.Admin.Profiles;

public class CategoryItemProfile : Profile
{
  public CategoryItemProfile()
  {
    CreateMap<CategoryItemDto, FrontCategoryItemDto>().ReverseMap();
  }
}
