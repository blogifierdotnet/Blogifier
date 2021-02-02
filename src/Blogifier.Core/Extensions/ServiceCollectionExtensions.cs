using Blogifier.Core.Data;
using Blogifier.Core.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace Blogifier.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlogDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("Blogifier");
            var conn = section.GetValue<string>("ConnString");
            var provider = section.GetValue<string>("DbProvider");

            services.AddDbContext<AppDbContext>(
                    options => _ = provider switch
                    {
                        "SQLite" => options.UseSqlite(
                            conn,
                            x => x.MigrationsAssembly("Blogifier.SQLiteMigrations")),

                        "SqlServer" => options.UseSqlServer(
                            conn,
                            x => x.MigrationsAssembly("Blogifier.SqlServerMigrations")),

                        "Postgres" => options.UseNpgsql(
                            conn,
                            x => x.MigrationsAssembly("Blogifier.PostgresMigrations")),

                        "MySql" => options.UseMySql(
                            conn,
                            new MySqlServerVersion(new Version(8, 0, 21)),
                            mySqlOptions =>
                            {
                                mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend);
                                mySqlOptions.MigrationsAssembly("Blogifier.MySqlMigrations");
                            }),

                        _ => throw new Exception($"Unsupported provider: {provider}")
                    });

            services.AddDatabaseDeveloperPageExceptionFilter();
            return services;
        }

        public static IServiceCollection AddBlogProviders(this IServiceCollection services)
        {
            services.AddScoped<IAuthorProvider, AuthorProvider>();
            services.AddScoped<IBlogProvider, BlogProvider>();
            services.AddScoped<IPostProvider, PostProvider>();
            services.AddScoped<IStorageProvider, StorageProvider>();
            services.AddScoped<IFeedProvider, FeedProvider>();
            services.AddScoped<ICategoryProvider, CategoryProvider>();
            services.AddScoped<IAnalyticsProvider, AnalyticsProvider>();
            services.AddScoped<INewsletterProvider, NewsletterProvider>();
            services.AddScoped<IEmailProvider, MailKitProvider>();
            services.AddScoped<IThemeProvider, ThemeProvider>();
            services.AddScoped<IRssImportProvider, RssImportProvider>();
            services.AddScoped<ISyndicationProvider, SyndicationProvider>();

            return services;
        }
    }
}
