using AutoMapper;
using Blogifier.Shared;

namespace Blogifier.Admin.Profiles;

public class PostEditorProfile : Profile
{
  public PostEditorProfile()
  {
    CreateMap<PostEditorDto, FrontPostImportDto>().ReverseMap();
    CreateMap<PostEditorDto, FrontPostEditorDto>().ReverseMap();
  }
}
