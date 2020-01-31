using Core;
using Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

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
            services.AddDbProvider(Configuration);

            services.AddSecurity();

            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(dispose: true));

            services.AddAppLocalization();

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
                services.AddSwagger();
            }
                        
            services.AddRazorPages(
                options => options.Conventions.AuthorizeFolder("/Admin")
            );

            //services.AddServerSideBlazor();

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
                //endpoints.MapBlazorHub();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
            });
        }
    }
}