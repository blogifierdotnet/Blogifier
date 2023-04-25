using Blogifier.Core.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApiAboutController : ControllerBase
{
  private readonly IAboutProvider _aboutProvider;

  public ApiAboutController(IAboutProvider aboutProvider)
  {
    _aboutProvider = aboutProvider;
  }

  [HttpGet]
  public async Task<AboutModel> GetAbout()
  {
    return await _aboutProvider.GetAboutModel();
  }
}
