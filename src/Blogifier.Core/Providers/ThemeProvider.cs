using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Core.Providers
{
	public interface IThemeProvider
	{
		Task<Dictionary<string, string>> GetSettings(string theme);
	}

	public class ThemeProvider : IThemeProvider
	{
		public async Task<Dictionary<string, string>> GetSettings(string theme)
		{
			var settings = new Dictionary<string, string>();
			settings.Add("one", "<div>the one</div>");
			settings.Add("two", "<div>the two</div>");
			return await Task.FromResult(settings);
		}
	}
}
