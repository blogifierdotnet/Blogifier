using Core;
using Core.Data;
using Core.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            SeedData(host);

            LoadSettings(host);

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
                catch { }
            }
        }

        static void LoadSettings(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<AppDbContext>();

                    foreach (var s in context.Settings)
                    {
                        if (s.SettingKey == "app-title") AppSettings.Title = s.SettingValue;
                        if (s.SettingKey == "app-desc") AppSettings.Description = s.SettingValue;
                        if (s.SettingKey == "app-logo") AppSettings.Logo = s.SettingValue;
                        if (s.SettingKey == "app-cover") AppSettings.Cover = s.SettingValue;
                        if (s.SettingKey == "app-theme") AppSettings.Theme = s.SettingValue;
                        if (s.SettingKey == "app-post-list-type") AppSettings.PostListType = s.SettingValue;
                        if (s.SettingKey == "app-items-per-page") AppSettings.ItemsPerPage = int.Parse(s.SettingValue);
                    }
                }
                catch { }
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}