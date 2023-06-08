using AutoMapper;
using Blogifier.Newsletters;
using Blogifier.Shared;

namespace Blogifier.Profiles;

public class NewsletterProfile : Profile
{
  public NewsletterProfile()
  {
    CreateMap<Newsletter, NewsletterDto>();
  }
}
