using Core.Data;
using Core.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAppSettings<T>(this IServiceCollection services, IConfigurationSection section) where T : class, new()
        {
            services.Configure<T>(section);
            services.AddTransient<IAppService<T>>(provider =>
            {
                var options = provider.GetService<IOptionsMonitor<T>>();
                return new AppService<T>(options);
            });
        }

        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            AddFileProviders(services);

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IDataService, DataService>();
            services.AddTransient<IFeedService, FeedService>();
            services.AddTransient<IStorageService, StorageService>();
            services.AddTransient<IImportService, ImportService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IWebService, WebService>();
            services.AddTransient<IEmailService, SendGridService>();

            services.AddTransient<UserManager<AppUser>>();

            return services;
        }

        static void AddFileProviders(IServiceCollection services)
        {
            try
            {
                services.Configure<RazorViewEngineOptions>(options =>
                {
                    foreach (var assembly in AppConfig.GetAssemblies(true))
                    {
                        var fileProvider = new EmbeddedFileProvider(assembly, assembly.GetName().Name);

                        // load themes from embedded provider
                        var content = fileProvider.GetDirectoryContents("");
                        if (content.Exists)
                        {
                            foreach (var item in content)
                            {
                                if (item.Name.StartsWith("Views.Themes"))
                                {
                                    if (AppConfig.EmbeddedThemes == null)
                                        AppConfig.EmbeddedThemes = new List<string>();

                                    var ar = item.Name.Split('.');
                                    if(ar.Length > 2 && !AppConfig.EmbeddedThemes.Contains(ar[2]))
                                    {
                                        if(assembly.GetName().Name.ToLower() != "app")
                                        {
                                            AppConfig.EmbeddedThemes.Add(ar[2]);
                                        }
                                    }
                                }
                            }
                        }

                        options.FileProviders.Add(fileProvider);
                    }
                });
            }
            catch { }
        }
    }
}