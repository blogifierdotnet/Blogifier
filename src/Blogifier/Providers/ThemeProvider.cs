using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Providers;

public class ThemeProvider
{
  public async Task<Dictionary<string, string>> GetSettings(string theme)
  {
    var settings = new Dictionary<string, string>
    {
      { "one", "<div>the one</div>" },
      { "two", "<div>the two</div>" }
    };
    return await Task.FromResult(settings);
  }
}
