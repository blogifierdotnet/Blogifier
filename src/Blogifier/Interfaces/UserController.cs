using AutoMapper;
using Blogifier.Identity;
using Blogifier.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogifier.Interfaces;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
  protected readonly IMapper _mapper;
  protected readonly IdentityManager _identityManager;

  public UserController(IMapper mapper, IdentityManager identityManager)
  {
    _mapper = mapper;
    _identityManager = identityManager;
  }

  [HttpGet("identity")]
  [Authorize]
  public IdentityUserDto GetInfo()
  {
    var identity = _identityManager.GetIdentityUser(User);
    var identityDto = _mapper.Map<IdentityUserDto>(identity);
    return identityDto;
  }
}
