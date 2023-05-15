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

  public UserController(IMapper mapper)
  {
    _mapper = mapper;
  }

  [HttpGet("identity")]
  [Authorize]
  public IdentityUserDto GetInfo()
  {
    var identity = IdentityUser.Analysis(User);
    var identityDto = _mapper.Map<IdentityUserDto>(identity);
    return identityDto;
  }
}
