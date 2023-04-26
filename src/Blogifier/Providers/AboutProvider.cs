using Blogifier.Data;
using Blogifier.Shared;
using System.Reflection;
using System.Threading.Tasks;

namespace Blogifier.Providers;

public interface IAboutProvider
{
  Task<AboutModel> GetAboutModel();
}

public class AboutProvider : IAboutProvider
{
  private readonly AppDbContext _db;

  public AboutProvider(AppDbContext db)
  {
    _db = db;
  }
  public async Task<AboutModel> GetAboutModel()
  {
    var model = new AboutModel
    {
      Version = typeof(AboutProvider)
           .GetTypeInfo()
           .Assembly
           .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
           .InformationalVersion,
      DatabaseProvider = _db.Database.ProviderName,
      OperatingSystem = System.Runtime.InteropServices.RuntimeInformation.OSDescription
    };
    return await Task.FromResult(model);
  }
}
