using Blogifier.Shared;
using Blogifier.Storages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Interfaces;

[Route("api/theme")]
[ApiController]
public class ThemeController : ControllerBase
{
  private readonly StorageManager _storageProvider;

  public ThemeController(StorageManager storageProvider)
  {
    _storageProvider = storageProvider;
  }

  [Authorize]
  [HttpGet("{theme}")]
  public async Task<ThemeSettings> GetThemeSettings(string theme)
  {
    return await _storageProvider.GetThemeSettingsAsync(theme);
  }

  [Authorize]
  [HttpPost("{theme}")]
  public async Task<bool> SaveThemeSettings(string theme, ThemeSettings settings)
  {
    return await _storageProvider.SaveThemeSettingsAsync(theme, settings);
  }
}
