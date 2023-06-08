using Blogifier.Core.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ThemeController : ControllerBase
	{
		private readonly IThemeProvider _themeProvider;
		private readonly IStorageProvider _storageProvider;

		public ThemeController(IThemeProvider themeProvider, IStorageProvider storageProvider)
		{
			_themeProvider = themeProvider;
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
}
