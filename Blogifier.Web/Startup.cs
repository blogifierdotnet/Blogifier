using Blogifier.Core.Middleware;
using Blogifier.Data;
using Blogifier.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace Blogifier
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
              .Enrich.FromLogContext()
              .WriteTo.RollingFile("Logs/blogifier-{Date}.txt", LogEventLevel.Warning)
              .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            System.Action<DbContextOptionsBuilder> databaseOptions = options => 
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));

            services.AddDbContext<ApplicationDbContext>(databaseOptions);

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(dispose: true));

            services.AddMvc()
            .ConfigureApplicationPartManager(p =>
            {
                foreach (var assembly in Core.Configuration.GetAssemblies())
                {
                    if (assembly.GetName().Name != "Blogifier.Web" && assembly.GetName().Name != "Blogifier.Core")
                    {
                        p.ApplicationParts.Add(new AssemblyPart(assembly));
                    }
                }
            });

            Core.Configuration.InitServices(services, databaseOptions, Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseETagger();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Blog}/{action=Index}/{id?}");
            });

            Core.Configuration.InitApplication(app, env);
        }
    }
}
