using AutoMapper;
using Blogifier.Shared;

namespace Blogifier.Admin.Profiles;

public class ImportProfile : Profile
{
  public ImportProfile()
  {
    CreateMap<ImportDto, FrontImportDto>().ReverseMap();
  }
}
