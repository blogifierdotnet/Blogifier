using Blogifier.Core;
using Blogifier.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sotsera.Blazor.Toaster.Core.Models;

namespace Blogifier
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBlogDatabase(Configuration);
            services.AddBlogSecurity();
            services.AddBlogLocalization();

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllersWithViews()
                .AddViewLocalization(); 
            
            services.AddRazorPages(options => 
                options.Conventions.AuthorizeFolder("/Admin")
                .AllowAnonymousToPage("/Admin/_Host")
            ).AddViewLocalization();

            services.AddServerSideBlazor();

            services.AddHttpContextAccessor();

            services.AddToaster(config =>
            {
                config.PositionClass = Defaults.Classes.Position.BottomRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = false;
            });

            services.AddBlogServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            AppSettings.WebRootPath = env.WebRootPath;
            AppSettings.ContentRootPath = env.ContentRootPath;

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseRequestLocalization();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Blog}/{action=Index}/{id?}"
                );
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/Admin/_Host");
            });
        }
    }
}