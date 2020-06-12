using Askmethat.Aspnet.JsonLocalizer.Extensions;
using Blogifier.Core.Api;
using Blogifier.Core.Data;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Blogifier.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBlogSettings<T>(this IServiceCollection services, IConfigurationSection section) where T : class, new()
        {
            services.Configure<T>(section);
            services.AddTransient<IAppService<T>>(provider =>
            {
                var options = provider.GetService<IOptionsMonitor<T>>();
                return new AppService<T>(options);
            });
        }

        public static IServiceCollection AddBlogServices(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IDataService, DataService>();
            services.AddTransient<IFeedService, FeedService>();
            services.AddTransient<IStorageService, StorageService>();
            services.AddTransient<IImportService, ImportService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IWebService, WebService>();
            services.AddTransient<INewsletterService, NewsletterService>();

            services.AddSingleton<CustomHttpClient>();

            services.AddTransient<UserManager<AppUser>>();

            AddBlogRepositories(services);

            return services;
        }

        public static IServiceCollection AddBlogDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("Blogifier");

            services.AddBlogSettings<AppItem>(section);

            if (section.GetValue<string>("DbProvider") == "SqlServer")
            {
                AppSettings.DbOptions = options => options.UseSqlServer(section.GetValue<string>("ConnString"));
            }
            else if (section.GetValue<string>("DbProvider") == "MySql")
            {
                AppSettings.DbOptions = options => options.UseMySql(section.GetValue<string>("ConnString"));
            }
            else if (section.GetValue<string>("DbProvider") == "Postgres")
            {
                AppSettings.DbOptions = options => options.UseNpgsql(section.GetValue<string>("ConnString"));
            }
            else
            {
                AppSettings.DbOptions = options => options.UseSqlite(section.GetValue<string>("ConnString"));
            }

            services.AddDbContext<AppDbContext>(AppSettings.DbOptions, ServiceLifetime.Scoped);

            return services;
        }

        public static IServiceCollection AddBlogLocalization(this IServiceCollection services)
        {
            var supportedCultures = new HashSet<CultureInfo>()
            {
                new CultureInfo("en-US"),
                new CultureInfo("es-ES"),
                new CultureInfo("pt-BR"),
                new CultureInfo("ru-RU"),
                new CultureInfo("zh-cn"),
                new CultureInfo("zh-tw")
            };

            services.AddJsonLocalization(options => {
                options.DefaultCulture = new CultureInfo("en-US");
                options.ResourcesPath = "Resources";
                options.SupportedCultureInfos = supportedCultures;
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                options.SupportedCultures = supportedCultures.ToArray();
                options.SupportedUICultures = supportedCultures.ToArray();
            });

            return services;
        }

        public static IServiceCollection AddBlogSecurity(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.AllowedUserNameCharacters = null;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
            return services;
        }

        private static void AddBlogRepositories(IServiceCollection services)
        {
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<ICustomFieldRepository, CustomFieldRepository>();
            services.AddScoped<IHtmlWidgetRepository, HtmlWidgetRepository>();
            services.AddScoped<INewsletterRepository, NewsletterRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICustomFieldRepository, CustomFieldRepository>();
            services.AddScoped<ICustomFieldRepository, CustomFieldRepository>();
            services.AddScoped<IStatsRepository, StatsRepository>();
        }
    }
}