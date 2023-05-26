using Blogifier.Data;
using Blogifier.Shared;
using System.Reflection;
using System.Threading.Tasks;

namespace Blogifier.Providers;

public class AboutProvider
{
  private readonly AppDbContext _dbContext;

  public AboutProvider(AppDbContext dbContext)
  {
    _dbContext = dbContext;
  }
  public async Task<AboutDto> GetAsync()
  {
    var model = new AboutDto
    {
      Version = typeof(AboutProvider)?.GetTypeInfo()?.Assembly?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion,
      DatabaseProvider = _dbContext.Database.ProviderName,
      OperatingSystem = System.Runtime.InteropServices.RuntimeInformation.OSDescription
    };
    return await Task.FromResult(model);
  }
}
