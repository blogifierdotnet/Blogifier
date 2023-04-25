using Blogifier.Core.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Interfaces;

[Route("api/theme")]
[ApiController]
public class ThemeController : ControllerBase
{
  private readonly IStorageProvider _storageProvider;

  public ThemeController(IStorageProvider storageProvider)
  {
    _storageProvider = storageProvider;
  }

  [Authorize]
  [HttpGet("{theme}")]
  public async Task<ThemeSettings> GetThemeSettings(string theme)
  {
    return await _storageProvider.GetThemeSettings(theme);
  }

  [Authorize]
  [HttpPost("{theme}")]
  public async Task<bool> SaveThemeSettings(string theme, ThemeSettings settings)
  {
    return await _storageProvider.SaveThemeSettings(theme, settings);
  }
}
