using Blogifier.Data;
using Blogifier.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blogifier.Extensions;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddBlogDatabase(this IServiceCollection services, IConfiguration configuration)
  {
    var section = configuration.GetSection("Blogifier");
    var conn = section.GetValue<string>("ConnString");

    if (section.GetValue<string>("DbProvider") == "SQLite")
      services.AddDbContext<AppDbContext>(o => o.UseSqlite(conn));

    if (section.GetValue<string>("DbProvider") == "SqlServer")
      services.AddDbContext<AppDbContext>(o => o.UseSqlServer(conn));

    if (section.GetValue<string>("DbProvider") == "Postgres")
      services.AddDbContext<AppDbContext>(o => o.UseNpgsql(conn));

    if (section.GetValue<string>("DbProvider") == "MySql")
      services.AddDbContext<AppDbContext>(o => o.UseMySql(conn, ServerVersion.AutoDetect(conn)));

    services.AddDatabaseDeveloperPageExceptionFilter();
    return services;
  }

  public static IServiceCollection AddBlogProviders(this IServiceCollection services)
  {
    services.AddScoped<IAuthorProvider, AuthorProvider>();
    services.AddScoped<BlogProvider>();
    services.AddScoped<IPostProvider, PostProvider>();
    services.AddScoped<IStorageProvider, StorageProvider>();
    services.AddScoped<IFeedProvider, FeedProvider>();
    services.AddScoped<ICategoryProvider, CategoryProvider>();
    services.AddScoped<IAnalyticsProvider, AnalyticsProvider>();
    services.AddScoped<INewsletterProvider, NewsletterProvider>();
    services.AddScoped<IEmailProvider, MailKitProvider>();
    services.AddScoped<IThemeProvider, ThemeProvider>();
    services.AddScoped<IImportProvider, ImportProvider>();
    services.AddScoped<IAboutProvider, AboutProvider>();
    services.AddSingleton<IMinioProvider, MinioProvider>();

    return services;
  }
}
