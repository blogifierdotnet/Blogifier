using Blogifier.Core.Common;
using Blogifier.Core.Data;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Repositories;
using Blogifier.Core.Middleware;
using Blogifier.Core.Services.FileSystem;
using Blogifier.Core.Services.Search;
using Blogifier.Core.Services.Custom;
using Blogifier.Core.Services.Routing;
using Blogifier.Core.Services.Syndication.Rss;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Blogifier.Core
{
    public class Configuration
    {
		public static void InitServices(IServiceCollection services, IConfiguration config = null)
		{
            if(config != null)
            {
                var loader = new AppSettingsLoader();
                loader.LoadFromConfigFile(config);
            }
                
            services.AddTransient<IRssService, RssService>();
			services.AddTransient<IBlogStorage, BlogStorage>();
            services.AddTransient<ISearchService, SearchService>();
            services.AddTransient<ICustomService, CustomService>();

            // add blog route from ApplicationSettings
            services.Configure<Microsoft.AspNetCore.Mvc.MvcOptions>(opt =>
                opt.UseBlogRoutePrefix(new Microsoft.AspNetCore.Mvc.RouteAttribute(ApplicationSettings.BlogRoute)));

            // add route constraint
            services.Configure<RouteOptions>(options =>
                options.ConstraintMap.Add("author", typeof(AuthorRouteConstraint)));

            AddDatabase(services);

			AddFileProviders(services);
		}

		public static void InitApplication(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			app.UseMiddleware<EmbeddedResources>();

			ApplicationSettings.WebRootPath = env.WebRootPath;
			ApplicationSettings.ContentRootPath = env.ContentRootPath;

            if (!ApplicationSettings.UseInMemoryDatabase)
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

		static void AddDatabase(IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();

            if (ApplicationSettings.UseInMemoryDatabase)
                services.AddDbContext<BlogifierDbContext>(options => options.UseInMemoryDatabase());
            else
                services.AddDbContext<BlogifierDbContext>(options => options.UseSqlServer(ApplicationSettings.ConnectionString));
        }

		static void AddFileProviders(IServiceCollection services)
		{
			var assemblyName = "Blogifier.Core";

			try
			{
				var assembly = Assembly.Load(new AssemblyName(assemblyName));

				services.Configure<RazorViewEngineOptions>(options =>
				{
                    // in some environments provider order matters
                    if(ApplicationSettings.PrependFileProvider)
					    options.FileProviders.Insert(0, new EmbeddedFileProvider(assembly, assemblyName));
                    else
					    options.FileProviders.Add(new EmbeddedFileProvider(assembly, assemblyName));
				});
			}
			catch { }
		}
	}
}