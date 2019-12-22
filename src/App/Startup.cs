using Askmethat.Aspnet.JsonLocalizer.Extensions;
using Core;
using Core.Data;
using Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Globalization;
using System.IO;

namespace App
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

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
            else if (section.GetValue<string>("DbProvider") == "Postgres")
            {
                AppSettings.DbOptions = options => options.UseNpgsql(section.GetValue<string>("ConnString"));
            }
            else
            {
                AppSettings.DbOptions = options => options.UseSqlite(section.GetValue<string>("ConnString"));
            }
            
            services.AddDbContext<AppDbContext>(AppSettings.DbOptions, ServiceLifetime.Scoped);

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

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                );
            });

            services.AddControllersWithViews()
                .AddViewLocalization()
                .ConfigureApplicationPartManager(p =>
                {
                    foreach (var assembly in AppConfig.GetAssemblies())
                    {
                        p.ApplicationParts.Add(new AssemblyPart(assembly));
                    }
                });

            if (Environment.IsDevelopment())
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
            }

            services.AddRazorPages(
                options => options.Conventions.AuthorizeFolder("/Admin")
            );
                                    
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot/themes/_active";
            });

            services.AddAppServices();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(setupAction =>
                {
                    setupAction.SwaggerEndpoint(
                        "/swagger/v1/swagger.json",
                        "Blogifier API"
                    );
                });
            }

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseRequestLocalization();        

            AppSettings.WebRootPath = Environment.WebRootPath;
            AppSettings.ContentRootPath = Environment.ContentRootPath;

            app.UseRouting();
            app.UseCors("AllowOrigin");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Blog}/{action=Index}/{id?}"
                );
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
            });
        }
    }
}