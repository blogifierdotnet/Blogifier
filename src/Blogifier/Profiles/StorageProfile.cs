using AutoMapper;
using Blogifier.Shared;
using Blogifier.Storages;

namespace Blogifier.Profiles;

public class StorageProfile : Profile
{
  public StorageProfile()
  {
    CreateMap<Storage, StorageDto>().ReverseMap();
    //CreateMap<StorageReference, StorageDto>()
    //  .IncludeMembers(m => m.Storage)
    //  .ReverseMap();
  }
}
