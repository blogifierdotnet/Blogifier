using AutoMapper;
using Blogifier.Models;
using Blogifier.Shared;

namespace Blogifier.Profiles;

public class CategoryItemProfile : Profile
{
  public CategoryItemProfile()
  {
    CreateMap<CategoryItem, CategoryItemDto>();
  }
}
