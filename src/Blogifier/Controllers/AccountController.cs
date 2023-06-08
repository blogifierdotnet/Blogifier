using Blogifier.Blogs;
using Blogifier.Identity;
using Blogifier.Options;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

[Route("account")]
public class AccountController : Controller
{
  protected readonly ILogger _logger;
  protected readonly UserManager _userManager;
  protected readonly SignInManager _signInManager;
  protected readonly BlogManager _blogManager;

  public AccountController(
    ILogger<AccountController> logger,
    UserManager userManager,
    SignInManager signInManager,
    BlogManager  blogManager)
  {
    _logger = logger;
    _userManager = userManager;
    _signInManager = signInManager;
    _blogManager = blogManager;
  }

  [HttpGet]
  public IActionResult Index([FromQuery] AccountModel parameter)
    => RedirectToAction("login", routeValues: parameter);

  [HttpGet("login")]
  public async Task<IActionResult> Login([FromQuery] AccountModel parameter)
  {
    var data = await _blogManager.GetBlogDataAsync();
    var model = new AccountLoginModel { RedirectUri = parameter.RedirectUri };
    return View($"~/Views/Themes/{data.Theme}/login.cshtml", model);
  }

  [HttpPost("login")]
  public async Task<IActionResult> LoginForm([FromForm] AccountLoginModel model)
  {
    if (ModelState.IsValid)
    {
      var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, lockoutOnFailure: true);
      if (result.Succeeded)
      {
        _logger.LogInformation("User logged in.");
        model.RedirectUri ??= "/";
        return LocalRedirect(model.RedirectUri);
      }
      model.ShowError = true;
    }
    var data = await _blogManager.GetBlogDataAsync();
    return View($"~/Views/Themes/{data.Theme}/login.cshtml", model);
  }

  [HttpGet("register")]
  public async Task<IActionResult> Register([FromQuery] AccountModel parameter)
  {
    var model = new AccountRegisterModel { RedirectUri = parameter.RedirectUri };
    var data = await _blogManager.GetBlogDataAsync();
    return View($"~/Views/Themes/{data.Theme}/register.cshtml", model);
  }

  [HttpPost("register")]
  public async Task<IActionResult> RegisterForm([FromForm] AccountRegisterModel model)
  {
    if (ModelState.IsValid)
    {
      var user = new UserInfo { UserName = model.UserName, Email = model.Email };
      var result = await _userManager.CreateAsync(user, model.Password);
      if (result.Succeeded)
      {
        return RedirectToAction("login", routeValues: new AccountModel { RedirectUri = model.RedirectUri });
      }
      model.ShowError = true;
    }
    var data = await _blogManager.GetBlogDataAsync();
    return View($"~/Views/Themes/{data.Theme}/register.cshtml", model);
  }
}
