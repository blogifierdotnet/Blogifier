using AutoMapper;
using Blogifier.Shared;

namespace Blogifier.Profiles;

public class CategoryProfile : Profile
{
  public CategoryProfile()
  {
    CreateMap<Category, CategoryDto>().ReverseMap();
  }
}
