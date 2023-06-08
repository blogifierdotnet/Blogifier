using Blogifier.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Interfaces;

[Route("api/about")]
[ApiController]
public class AboutController : ControllerBase
{
  private readonly AboutProvider _aboutProvider;

  public AboutController(AboutProvider aboutProvider)
  {
    _aboutProvider = aboutProvider;
  }

  [HttpGet]
  public async Task<AboutModel> GetAbout()
  {
    return await _aboutProvider.GetAboutModel();
  }
}
