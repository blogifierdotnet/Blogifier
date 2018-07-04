using Core;
using Core.Data;
using Core.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                

                var context = services.GetRequiredService<AppDbContext>();

                // load application settings from appsettings.json
                var app = services.GetRequiredService<IAppSettingsService<AppItem>>();
                AppConfig.SetSettings(app.Value);

                if (!context.BlogPosts.Any())
                {
                    services.GetRequiredService<IStorageService>().Reset();
                    context.Seed(services);
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
    }
}