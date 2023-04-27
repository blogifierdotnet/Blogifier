using Blogifier.Identity;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

[Route("account")]
public class AccountController : Controller
{
  private readonly ILogger _logger;
  private readonly BlogifierOptions _options;
  private readonly UserManager _userManager;
  private readonly SignInManager _signInManager;

  public AccountController(
    ILogger<AccountController> logger,
    IOptions<BlogifierOptions> options,
    UserManager userManager,
    SignInManager signInManager)
  {
    _logger = logger;
    _options = options.Value;
    _userManager = userManager;
    _signInManager = signInManager;
  }

  [HttpGet]
  public IActionResult Index([FromQuery] AccountModel parameter)
    => RedirectToAction("login", routeValues: parameter);

  [HttpGet("login")]
  public IActionResult Login([FromQuery] AccountModel parameter)
  {
    var model = new AccountLoginModel { RedirectUri = parameter.RedirectUri };
    return View($"~/Views/Themes/{_options.Theme}/login.cshtml", model);
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
    return View($"~/Views/Themes/{_options.Theme}/login.cshtml", model);
  }

  [HttpGet("register")]
  public IActionResult Register([FromQuery] AccountModel parameter)
  {
    var model = new AccountRegisterModel { RedirectUri = parameter.RedirectUri, };
    return View($"~/Views/Themes/{_options.Theme}/register.cshtml", model);
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
    return View($"~/Views/Themes/{_options.Theme}/register.cshtml", model);
  }
}
