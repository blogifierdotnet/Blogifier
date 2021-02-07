using Blogifier.Shared;
using System.Reflection;
using System.Threading.Tasks;

namespace Blogifier.Core.Providers
{
   public interface IAboutProvider
	{
      Task<AboutModel> GetAboutModel();
   }

	public class AboutProvider : IAboutProvider
	{
      public async Task<AboutModel> GetAboutModel()
		{
         var model = new AboutModel();

         model.Version = typeof(AboutProvider)
                .GetTypeInfo()
                .Assembly
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                .InformationalVersion;

         model.OperatingSystem = System.Runtime.InteropServices.RuntimeInformation.OSDescription;

         return await Task.FromResult(model);
      }
   }
}
