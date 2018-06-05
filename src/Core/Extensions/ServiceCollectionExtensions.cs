using Core.Data;
using Core.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAppSettings<T>(this IServiceCollection services, IConfigurationSection section) where T : class, new()
        {
            services.Configure<T>(section);
            services.AddTransient<IAppSettingsServices<T>>(provider =>
            {
                var environment = provider.GetService<IHostingEnvironment>();
                var options = provider.GetService<IOptionsMonitor<T>>();
                return new AppSettingsService<T>(environment, options, section.Key);
            });
        }

        public static void AddAppDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("Blogifier");

            if (section.GetValue<string>("DbProvider") == "SQLite")
            {
                services.AddDbContext<AppDbContext>(o => o.UseSqlite(section.GetValue<string>("ConnString")));
            }
            else if (section.GetValue<string>("DbProvider") == "SqlServer")
            {
                services.AddDbContext<AppDbContext>(o => o.UseSqlServer(section.GetValue<string>("ConnString")));
            }
            else
            {
                services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("Blogifier"));
            }

            services.AddAppSettings<AppItem>(section);
        }

        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            AddFileProviders(services);

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<ISearchService, SearchService>();
            services.AddTransient<ISyndicationService, SyndicationService>();
            services.AddTransient<IStorageService, StorageService>();

            services.AddTransient<UserManager<AppUser>>();

            return services;
        }

        static void AddFileProviders(IServiceCollection services)
        {
            try
            {
                services.Configure<RazorViewEngineOptions>(options =>
                {
                    foreach (var assembly in AppConfig.GetAssemblies())
                    {
                        options.FileProviders.Add(new EmbeddedFileProvider(assembly, assembly.GetName().Name));
                    }
                });
            }
            catch { }
        }
    }
}