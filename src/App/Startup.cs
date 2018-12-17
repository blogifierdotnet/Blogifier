using Askmethat.Aspnet.JsonLocalizer.Extensions;
using Core;
using Core.Data;
using Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System.Globalization;

namespace App
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
              .Enrich.FromLogContext()
              .WriteTo.RollingFile("Logs/{Date}.txt", LogEventLevel.Warning)
              .CreateLogger();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var section = Configuration.GetSection("Blogifier");

            services.AddAppSettings<AppItem>(section);

            if (section.GetValue<string>("DbProvider") == "SqlServer")
            {
                AppSettings.DbOptions = options => options.UseSqlServer(section.GetValue<string>("ConnString"));
            }
            else if (section.GetValue<string>("DbProvider") == "MySql")
            {
                AppSettings.DbOptions = options => options.UseMySql(section.GetValue<string>("ConnString"));
            }
            else
            {
                AppSettings.DbOptions = options => options.UseSqlite(section.GetValue<string>("ConnString"));
            }
            
            services.AddDbContext<AppDbContext>(AppSettings.DbOptions, ServiceLifetime.Transient);

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

            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(dispose: true));

            services.AddJsonLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ru-RU"),
                    new CultureInfo("zh-cn"),
                    new CultureInfo("zh-tw")
                };

                options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddMvc()
            .AddViewLocalization()
            .ConfigureApplicationPartManager(p =>
            {
                foreach (var assembly in AppConfig.GetAssemblies())
                {
                    p.ApplicationParts.Add(new AssemblyPart(assembly));
                }
            })
            .AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizeFolder("/Admin");
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAppServices();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseRequestLocalization();

            AppSettings.WebRootPath = env.WebRootPath;
            AppSettings.ContentRootPath = env.ContentRootPath;

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Blog}/{action=Index}/{id?}");
            });
        }
    }
}