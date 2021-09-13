using Blogifier.Core.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Blogifier
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration()
                  .Enrich.FromLogContext()
                  .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                  .CreateLogger();

            Log.Warning("Application start");
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Log.Warning("Start configure services");

            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie();

            services.AddCors(o => o.AddPolicy("BlogifierPolicy", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddBlogDatabase(Configuration);

            services.AddBlogProviders();

            
            services.AddControllersWithViews();
            services.AddRazorPages();

            Log.Warning("Done configure services");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();
            app.UseCors("BlogifierPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                      name: "default",
                      pattern: "{controller=Home}/{action=Index}/{id?}"
                 );
                endpoints.MapRazorPages();
                endpoints.MapFallbackToFile("admin/{*path:nonfile}", "index.html");
                endpoints.MapFallbackToFile("account/{*path:nonfile}", "index.html");
            });
        }
    }
}
