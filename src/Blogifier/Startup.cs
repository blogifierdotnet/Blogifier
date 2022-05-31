using System.Security.AccessControl;
using System.Reflection;
using Blogifier.Core.Extensions;
using Blogifier.Core.Providers;
using Blogifier.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpLogging;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System;
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
            System.Console.WriteLine(Configuration.GetSection("Oidc").GetValue<string>("ClientId"));
            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddHttpContextAccessor();
            services.AddHttpLogging(httpLogging =>
            {
                httpLogging.LoggingFields = HttpLoggingFields.All;
                // httpLogging.RequestHeaders.Add("Cookie");
            });
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "cookie";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("cookie", options =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
            })
            .AddOpenIdConnect("oidc", options =>
            {
                Configuration.Bind("Oidc", options);
                options.Scope.Add("profile");
                options.Scope.Add("avatar");
                options.Scope.Add("email");
                options.Scope.Add("roles");
                options.Scope.Add("comments.read");
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.ClaimActions.MapJsonKey("picture", "picture");
                options.ClaimActions.MapJsonKey("role", "role");
                //options.Events.OnSignedOutCallbackRedirect();
            });

            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, BlogifierServerAuthStateProvider>();
            services.AddCors(o => o.AddPolicy("BlogifierPolicy", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddHttpClient("Local", httpClient =>
            {
                httpClient.BaseAddress = new Uri(Configuration["ASPNETCORE_URLS"]);
            });
            // services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(Configuration["ASPNETCORE_URLS"]) });

            services.AddBlogDatabase(Configuration);
            services.AddBlogProviders();
            services.AddScoped<IMessageService, MessageService>();

            services.AddRazorPages();
            services.AddServerSideBlazor()
                .AddCircuitOptions(options => { options.DetailedErrors = true; });
            // services.AddServerSideBlazor();
            services.AddControllersWithViews();
            //Add Detailed Error information to client
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
            // app.UsePathBase("/themes/standard");
            app.UseBlazorFrameworkFiles();
            app.UseHttpLogging();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseCors("BlogifierPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            // app.UseCustomSyncMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                      name: "default",
                      pattern: "{controller=Home}/{action=Index}/{id?}"
                 );
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToFile("admin/{*path:nonfile}", "index.html");
                endpoints.MapFallbackToFile("account/{*path:nonfile}", "index.html");
            });
        }
    }
    public static class CustomSyncMiddleware
    {
        public static void UseCustomSyncMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<SyncMiddleware>();
        }
    }
}
