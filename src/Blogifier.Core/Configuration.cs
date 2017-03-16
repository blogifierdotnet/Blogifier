using System.Reflection;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Blogifier.Core
{
	public class Configuration
    {
		public static void InitServices(IServiceCollection services)
		{
			AddFileProviders(services);
		}

		static void AddFileProviders(IServiceCollection services)
		{
			var assemblyName = "Blogifier.Core";
			var assembly = Assembly.Load(new AssemblyName(assemblyName));

			services.Configure<RazorViewEngineOptions>(options =>
			{
				options.FileProviders.Insert(0, new EmbeddedFileProvider(assembly, assemblyName));
			});
		}
	}
}
