using Blogifier.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogifier.Interfaces;


[Route("api/token")]
[ApiController]
public class TokenController : ControllerBase
{

  [HttpGet("userinfo")]
  [Authorize]
  public BlogifierClaims? Get() => BlogifierClaims.Analysis(User);
}
