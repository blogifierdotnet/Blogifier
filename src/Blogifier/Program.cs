using Blogifier.Core.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Linq;

namespace Blogifier
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();

			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				var dbContext = services.GetRequiredService<AppDbContext>();

				try
				{
					if (dbContext.Database.GetPendingMigrations().Any())
						dbContext.Database.Migrate();
				}
				catch { }
			}

			host.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			 Host.CreateDefaultBuilder(args)
				  .ConfigureWebHostDefaults(webBuilder =>
				  {
					  webBuilder
					  .UseContentRoot(Directory.GetCurrentDirectory())
					  .UseIISIntegration()
					  .UseStartup<Startup>();
				  });
	}
}
