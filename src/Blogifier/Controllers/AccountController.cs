using Blogifier.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

public class AccountController : Controller
{
  private readonly BlogProvider _blogProvider;
  public AccountController(BlogProvider blogProvider)
  {
    _blogProvider = blogProvider;
  }

  [HttpGet("/account")]
  public IActionResult Index([FromQuery] AccountModel parameter)
    => RedirectToAction("login", routeValues: parameter);

  [HttpGet("/account/login")]
  public async Task<IActionResult> Login([FromQuery] AccountModel parameter)
  {
    var blog = await _blogProvider.FirstOrDefaultAsync();
    if (blog == null) return RedirectToAction("register", routeValues: parameter);
    var model = new AccountLoginModel { RedirectUri = parameter.RedirectUri };
    return View($"~/Views/Themes/{blog.Theme}/login.cshtml", model);
  }
}
