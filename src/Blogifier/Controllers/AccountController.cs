using Blogifier.Identity;
using Blogifier.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

[Route("account")]
public class AccountController : Controller
{
  private readonly ILogger _logger;
  private readonly SignInManager _signInManager;
  private readonly BlogProvider _blogProvider;
  public AccountController(ILogger<AccountController> logger, SignInManager signInManager, BlogProvider blogProvider)
  {
    _logger = logger;
    _signInManager = signInManager;
    _blogProvider = blogProvider;
  }

  [HttpGet]
  public IActionResult Index([FromQuery] AccountModel parameter)
    => RedirectToAction("login", routeValues: parameter);

  [HttpGet("login")]
  public async Task<IActionResult> Login([FromQuery] AccountModel parameter)
  {
    var blog = await _blogProvider.FirstOrDefaultAsync();
    if (blog == null) return RedirectToAction("register", routeValues: parameter);
    var model = new AccountLoginModel { RedirectUri = parameter.RedirectUri, Theme = blog.Theme };
    return View($"~/Views/Themes/{blog.Theme}/login.cshtml", model);
  }

  [HttpPost("login")]
  public async Task<IActionResult> LoginForm([FromForm] AccountLoginModel model)
  {
    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, lockoutOnFailure: true);
    if (result.Succeeded)
    {
      _logger.LogInformation("User logged in.");
      model.RedirectUri ??= "/";
      return LocalRedirect(model.RedirectUri);
    }
    model.ShowError = true;
    return View($"~/Views/Themes/{model.Theme}/login.cshtml", model);
  }

  [HttpGet("register")]
  public async Task<IActionResult> Register([FromQuery] AccountModel parameter)
  {
    var blog = await _blogProvider.FirstOrDefaultAsync();
    var theme = blog != null ? blog.Theme : "standard";
    var model = new AccountRegisterModel
    {
      RedirectUri = parameter.RedirectUri,
      Theme = theme,
    };
    return View($"~/Views/Themes/{theme}/register.cshtml", model);
  }
}
