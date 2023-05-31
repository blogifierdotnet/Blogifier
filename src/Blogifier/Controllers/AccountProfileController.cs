using Blogifier.Blogs;
using Blogifier.Identity;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

[Authorize]
[Route("account/profile")]
public class AccountProfileController : Controller
{
  private readonly UserProvider _userProvider;
  private readonly SignInManager _signInManager;
  private readonly BlogManager _blogManager;

  public AccountProfileController(
    UserProvider userProvider,
    SignInManager signInManager,
    BlogManager blogManager)
  {
    _blogManager = blogManager;
    _signInManager = signInManager;
    _userProvider = userProvider;
  }

  [HttpGet]
  public async Task<IActionResult> Profile([FromQuery] AccountModel parameter)
  {
    var userId = User.FirstUserId();
    var user = await _userProvider.FindByIdAsync(userId);
    var model = new AccountProfileEditModel
    {
      RedirectUri = parameter.RedirectUri,
      IsProfile = true,
      Email = user.Email,
      NickName = user.NickName,
      Avatar = user.Avatar,
      Bio = user.Bio,
    };
    var data = await _blogManager.GetAsync();
    return View($"~/Views/Themes/{data.Theme}/profile.cshtml", model);
  }

  [HttpPost]
  public async Task<IActionResult> ProfileForm([FromForm] AccountProfileEditModel model)
  {
    if (ModelState.IsValid)
    {
      var userId = User.FirstUserId();
      var input = new UserDto
      {
        Id = userId,
        Avatar = model.Avatar,
        Bio = model.Bio,
        Email = model.Email,
        NickName = model.NickName,
      };
      var user = await _userProvider.UpdateAsync(input);
      await _signInManager.SignInAsync(user, isPersistent: true);
    }
    var data = await _blogManager.GetAsync();
    return View($"~/Views/Themes/{data.Theme}/profile.cshtml", model);
  }
}
