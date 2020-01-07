using Core.Data;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
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

            AddAppRepositories(services);

            return services;
        }

        static void AddFileProviders(IServiceCollection services)
        {
            try
            {
                services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
                {
                    foreach (var assembly in AppConfig.GetAssemblies(true))
                    {
                        var fileProvider = new EmbeddedFileProvider(assembly, assembly.GetName().Name);
                        options.FileProviders.Add(fileProvider);
                    }
                });
            }
            catch { }
        }

        private static void AddAppRepositories(IServiceCollection services)
        {
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<ICustomFieldRepository, CustomFieldRepository>();
            services.AddScoped<IHtmlWidgetRepository, HtmlWidgetRepository>();
            services.AddScoped<INewsletterRepository, NewsletterRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICustomFieldRepository, CustomFieldRepository>();
            services.AddScoped<ICustomFieldRepository, CustomFieldRepository>();
        }
    }
}