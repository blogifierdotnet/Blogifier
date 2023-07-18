using Blogifier.Identity;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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

  [HttpGet("{id:int}")]
  public async Task<UserInfoDto?> GetAsync([FromRoute] int id)
  {
    return await _userProvider.GetAsync(id);
  }

  [HttpPut("{id:int?}")]
  public async Task<IActionResult> EditorAsync([FromRoute] int? id, [FromBody] UserEditorDto input, [FromServices] UserManager userManager)
  {
    if (!id.HasValue)
    {
      var user = new UserInfo(input.UserName)
      {
        NickName = input.NickName,
        Email = input.Email,
        Avatar = input.Avatar,
        Bio = input.Bio,
        Type = input.Type,
      };
      var result = await userManager.CreateAsync(user, input.Password!);
      if (!result.Succeeded)
      {
        var error = result.Errors.First();
        return Problem(detail: error.Description, title: error.Code);
      }
    }
    else
    {
      var user = await _userProvider.FindAsync(id.Value);
      user.NickName = input.NickName;
      user.Avatar = input.Avatar;
      user.Bio = input.Bio;
      user.Type = input.Type;
      var result = await userManager.UpdateAsync(user);
      if (result.Succeeded)
      {
        if (!string.IsNullOrEmpty(input.Password))
        {
          var token = await userManager.GeneratePasswordResetTokenAsync(user);
          result = await userManager.ResetPasswordAsync(user, token, input.Password);
          if (result.Succeeded) return Ok();
        }
        return Ok();
      }
      var error = result.Errors.First();
      return Problem(detail: error.Description, title: error.Code);
    }

    return Ok();
  }

}
