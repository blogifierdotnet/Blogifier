using AutoMapper;
using Blogifier.Newsletters;
using Blogifier.Shared;

namespace Blogifier.Profiles;

public class MailSettingProfile : Profile
{
  public MailSettingProfile()
  {
    CreateMap<MailSettingData, MailSettingDto>().ReverseMap();
  }
}
