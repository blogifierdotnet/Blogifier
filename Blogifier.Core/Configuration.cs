using Blogifier.Core.Common;
using Blogifier.Core.Data;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Repositories;
using Blogifier.Core.Middleware;
using Blogifier.Core.Services;
using Blogifier.Core.Services.Data;
using Blogifier.Core.Services.Email;
using Blogifier.Core.Services.FileSystem;
using Blogifier.Core.Services.Routing;
using Blogifier.Core.Services.Search;
using Blogifier.Core.Services.Syndication.Rss;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Blogifier.Core
{
    public class Configuration
    {
		public static void InitServices(IServiceCollection services, Action<DbContextOptionsBuilder> databaseOptions = null, IConfiguration config = null)
		{   
            if(config != null)
            {
                LoadFromConfigFile(config);
            }
            services.AddTransient<IRssService, RssService>();
			services.AddTransient<IBlogStorage, BlogStorage>();
            services.AddTransient<ISearchService, SearchService>();
            services.AddTransient<IDataService, DataService>();  
            services.AddTransient<IComponentHelper, ComponentHelper>();
            services.AddTransient<IEmailService, SendGridService>();
            services.AddTransient<IConfigService, ConfigService>();

            // add blog route from ApplicationSettings
            services.Configure<Microsoft.AspNetCore.Mvc.MvcOptions>(opt =>
                opt.UseBlogRoutePrefix(new Microsoft.AspNetCore.Mvc.RouteAttribute(ApplicationSettings.BlogRoute)));

            // add route constraint
            services.Configure<RouteOptions>(options =>
                options.ConstraintMap.Add("author", typeof(AuthorRouteConstraint)));

            if (databaseOptions != null)
            {
                ApplicationSettings.DatabaseOptions = databaseOptions;
            }

            AddDatabase(services);

			AddFileProviders(services);
		}

		public static void InitApplication(IApplicationBuilder app, IHostingEnvironment env)
		{
            app.UseMiddleware<AppSettingsLoader>();
			app.UseMiddleware<EmbeddedResources>();     

            ApplicationSettings.WebRootPath = env.WebRootPath;
			ApplicationSettings.ContentRootPath = env.ContentRootPath;

            if (!ApplicationSettings.UseInMemoryDatabase && ApplicationSettings.InitializeDatabase)
            {
                try
                {
                    using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                    {
                        var db = scope.ServiceProvider.GetService<BlogifierDbContext>().Database;
                        db.EnsureCreated();
                        if (db.GetPendingMigrations() != null)
                        {
                            db.Migrate();
                        }
                    }
                }
                catch { }
            }
        }

        public static IEnumerable<Assembly> GetAssemblies()
        {
            var assemblies = new List<Assembly>();
            try
            {
                foreach (string dll in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.TopDirectoryOnly))
                {
                    try
                    {
                        var assembly = Assembly.LoadFile(dll);
                        var product = assembly.GetCustomAttribute<AssemblyProductAttribute>().Product;

                        if (!string.IsNullOrEmpty(product) && product.StartsWith("Blogifier."))
                        {
                            if (!product.StartsWith("Blogifier.Web"))
                            {
                                assemblies.Add(assembly);
                            }
                        }
                    }
                    catch (FileLoadException) { }
                    catch (BadImageFormatException) { }
                }
            }
            catch { }
            return assemblies;
        }

        static void AddDatabase(IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<BlogifierDbContext>(ApplicationSettings.DatabaseOptions);
        }

		static void AddFileProviders(IServiceCollection services)
		{
            try
            {
                services.Configure<RazorViewEngineOptions>(options =>
                {
                    foreach (var assembly in GetAssemblies())
                    {
                        options.FileProviders.Add(new EmbeddedFileProvider(assembly, assembly.GetName().Name));
                    }
                });
            }
            catch { }
        }

        static void LoadFromConfigFile(IConfiguration config)
        {
            try
            {
                if (config != null)
                {
                    if (!string.IsNullOrEmpty(config.GetConnectionString("DefaultConnection")))
                        ApplicationSettings.ConnectionString = config.GetConnectionString("DefaultConnection");

                    var section = config.GetSection("Blogifier");
                    if (section != null)
                    {
                        // system settings

                        if (section["BlogRoute"] != null)
                            ApplicationSettings.BlogRoute = section.GetValue<string>("BlogRoute");

                        if (section["UseInMemoryDatabase"] != null)
                            ApplicationSettings.UseInMemoryDatabase = section.GetValue<bool>("UseInMemoryDatabase");

                        if (section["InitializeDatabase"] != null)
                            ApplicationSettings.InitializeDatabase = section.GetValue<bool>("InitializeDatabase");

                        if (section["ConnectionString"] != null)
                            ApplicationSettings.ConnectionString = section.GetValue<string>("ConnectionString");

                        if (section["EnableLogging"] != null)
                            ApplicationSettings.EnableLogging = section.GetValue<bool>("EnableLogging");

                        if (section["BlogStorageFolder"] != null)
                            ApplicationSettings.BlogStorageFolder = section.GetValue<string>("BlogStorageFolder");

                        if (section["BlogAdminFolder"] != null)
                            ApplicationSettings.BlogAdminFolder = section.GetValue<string>("BlogAdminFolder");

                        if (section["BlogThemesFolder"] != null)
                            ApplicationSettings.BlogThemesFolder = section.GetValue<string>("BlogThemesFolder");

                        if (section["SupportedStorageFiles"] != null)
                            BlogSettings.SupportedStorageFiles = section.GetValue<string>("SupportedStorageFiles");

                        // troubleshooting

                        if (section["AddContentTypeHeaders"] != null)
                            ApplicationSettings.AddContentTypeHeaders = section.GetValue<bool>("AddContentTypeHeaders");

                        if (section["AddContentLengthHeaders"] != null)
                            ApplicationSettings.AddContentLengthHeaders = section.GetValue<bool>("AddContentLengthHeaders");

                        if (section["PrependFileProvider"] != null)
                            ApplicationSettings.PrependFileProvider = section.GetValue<bool>("PrependFileProvider");
                    }
                }
            }
            catch { }
        }
    }
}