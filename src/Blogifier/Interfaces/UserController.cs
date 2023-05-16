using AutoMapper;
using Blogifier.Identity;
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
  public BlogifierClaims? GetInfo() => BlogifierClaims.Analysis(User);
}
