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
  private readonly BlogManager _blogManager;
  public AccountProfileController(UserProvider userProvider, BlogManager blogManager)
  {
    _blogManager = blogManager;
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
      User = user
    };
    var data = await _blogManager.GetAsync();
    return View($"~/Views/Themes/{data.Theme}/profile.cshtml", model);
  }
}
