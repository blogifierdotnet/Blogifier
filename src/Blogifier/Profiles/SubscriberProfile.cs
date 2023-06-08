using AutoMapper;
using Blogifier.Newsletters;
using Blogifier.Shared;

namespace Blogifier.Profiles;

public class SubscriberProfile : Profile
{
  public SubscriberProfile()
  {
    CreateMap<Subscriber, SubscriberDto>();
    CreateMap<SubscriberApplyDto, Subscriber>();
  }
}
