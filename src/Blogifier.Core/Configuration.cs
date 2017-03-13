using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.FileProviders;

namespace Blogifier.Core
{
	public class Configuration
    {
		public static void InitApplication(IApplicationBuilder app)
		{
			//app.UseStaticFiles();
			//app.UseMvc(routes =>
			//{
			//	routes.MapRoute(
			//		name: "Blogifier",
			//		template: "{controller=Blog}/{action=Index}/{id?}");
			//});
		}

		public static void AddCoreServices(IServiceCollection services)
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

			//var dependencies = DependencyContext.Default.RuntimeLibraries;
			//foreach (var library in dependencies)
			//{
			//	System.Diagnostics.Debug.WriteLine(library.Name);

			//	if (library.Name.StartsWith("Blogifier", StringComparison.OrdinalIgnoreCase))
			//	{
			//		var assembly = Assembly.Load(new AssemblyName(library.Name));
			//		var metadata = assembly.GetCustomAttributes<AssemblyMetadataAttribute>();

			//		foreach (var attribute in metadata)
			//		{
			//			if (attribute.Key == "BlogifierModule" && attribute.Value == "True")
			//			{
			//				services.Configure<RazorViewEngineOptions>(options =>
			//				{
			//					options.FileProviders.Insert(0, new EmbeddedFileProvider(assembly, library.Name));
			//				});
			//			}
			//		}
			//	}
			//}
		}
	}
}
