using Core;
using Core.Data;
using Core.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            SeedData(host);
            host.Run();
        }

        static void SeedData(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    UserManager<AppUser> userMgr = (UserManager<AppUser>)
                        services.GetRequiredService(typeof(UserManager<AppUser>));

                    var context = services.GetRequiredService<AppDbContext>();

                    var storage = services.GetRequiredService<IStorageService>();

                    if(!context.BlogPosts.Any())
                    {
                        storage.Reset();
                        DbInitializer.Initialize(context, userMgr);
                    }

                    // load application settings from appsettings.json
                    var app = services.GetRequiredService<IAppSettingsServices<AppItem>>();
                    AppConfig.SetSettings(app.Value);
                }
                catch (Exception ex)
                {
                    //TODO: log exception
                    var msg = ex.Message;
                }
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
    }
}