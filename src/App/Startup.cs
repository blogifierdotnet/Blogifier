using Core;
using Core.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            AddDb(services);

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc()
            .ConfigureApplicationPartManager(p =>
            {
                foreach (var assembly in AppConfig.GetAssemblies())
                {
                    p.ApplicationParts.Add(new AssemblyPart(assembly));
                }
            });

            services.AddCore();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseStaticFiles();

            AppSettings.WebRootPath = env.WebRootPath;
            AppSettings.ContentRootPath = env.ContentRootPath;

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Blog}/{action=Index}/{id?}");
            });
        }

        void AddDb(IServiceCollection services)
        {
            var section = Configuration.GetSection("Blogifier");

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
        }
    }
}