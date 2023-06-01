using Blogifier.Identity;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Interfaces;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
  private readonly UserProvider _userProvider;

  public UserController(UserProvider userProvider)
  {
    _userProvider = userProvider;
  }

  [HttpGet("items")]
  public async Task<IEnumerable<UserInfoDto>> GetItemsAsync()
  {
    return await _userProvider.GetAsync();
  }
}
