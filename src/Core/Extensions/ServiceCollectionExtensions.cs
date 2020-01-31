using Askmethat.Aspnet.JsonLocalizer.Extensions;
using Core.Data;
using Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System;
using System.Globalization;
using System.IO;

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

        public static IServiceCollection AddDbProvider(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("Blogifier");

            services.AddAppSettings<AppItem>(section);

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

        public static IServiceCollection AddAppLocalization(this IServiceCollection services)
        {
            services.AddJsonLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("es-ES"),
                    new CultureInfo("pt-BR"),
                    new CultureInfo("ru-RU"),
                    new CultureInfo("zh-cn"),
                    new CultureInfo("zh-tw")
                };

                options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            return services;
        }

        public static IServiceCollection AddSecurity(this IServiceCollection services)
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

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("v1",
                new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Blogifier API",
                    Version = "v1"
                });
                setupAction.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "CoreAPI.xml"));
            });
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