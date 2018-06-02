using Core.Data;
using Core.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

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

                    DbInitializer.Initialize(context, userMgr, storage);
                }
                catch (Exception ex)
                {
                    //TODO: log exception
                    var msg = ex.Message;
                }
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}