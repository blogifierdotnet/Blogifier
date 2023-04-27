using Blogifier.Identity;
using Blogifier.Options;
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
  private readonly BlogifierConstant _options;
  private readonly UserManager _userManager;
  private readonly SignInManager _signInManager;
  private readonly OptionManager _optionManager;

  public AccountController(
    ILogger<AccountController> logger,
    IOptions<BlogifierConstant> options,
    UserManager userManager,
    SignInManager signInManager,
    OptionManager optionManager)
  {
    _logger = logger;
    _options = options.Value;
    _userManager = userManager;
    _signInManager = signInManager;
    _optionManager = optionManager;
  }

  [HttpGet]
  public IActionResult Index([FromQuery] AccountModel parameter)
    => RedirectToAction("login", routeValues: parameter);

  [HttpGet("login")]
  public async Task<IActionResult> Login([FromQuery] AccountModel parameter)
  {
    var model = new AccountLoginModel { RedirectUri = parameter.RedirectUri };
    var theme = await _optionManager.GetThemeValueAsync();
    return View($"~/Views/Themes/{theme}/login.cshtml", model);
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
    var theme = await _optionManager.GetThemeValueAsync();
    return View($"~/Views/Themes/{theme}/login.cshtml", model);
  }

  [HttpGet("register")]
  public async Task<IActionResult> Register([FromQuery] AccountModel parameter)
  {
    var model = new AccountRegisterModel { RedirectUri = parameter.RedirectUri };
    var theme = await _optionManager.GetThemeValueAsync();
    return View($"~/Views/Themes/{theme}/register.cshtml", model);
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
    var theme = await _optionManager.GetThemeValueAsync();
    return View($"~/Views/Themes/{theme}/register.cshtml", model);
  }
}
