using Blogifier;
using Blogifier.Data;
using Blogifier.Extensions;
using Blogifier.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Linq;

var corsString = "BlogifierPolicy";
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, builder) =>
  builder.ReadFrom.Configuration(context.Configuration).Enrich.FromLogContext());
builder.Services.AddHttpClient();
builder.Services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
builder.Services.AddScoped<UserClaimsPrincipalFactory>();
builder.Services.AddIdentity<UserInfo, RoleInfo>(options =>
{
  options.Password.RequireUppercase = false;
  options.Password.RequireNonAlphanumeric = false;
  options.ClaimsIdentity.UserIdClaimType = UserInfo.ClaimTypes.UserId;
  options.ClaimsIdentity.UserNameClaimType = UserInfo.ClaimTypes.UserName;
  options.ClaimsIdentity.SecurityStampClaimType = UserInfo.ClaimTypes.SecurityStamp;
}).AddUserManager<UserManager>()
  .AddRoleManager<RoleManager>()
  .AddEntityFrameworkStores<AppDbContext>()
  .AddDefaultTokenProviders()
  .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory>();

builder.Services
  .AddAuthentication(options => options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme)
  .AddCookie();

builder.Services.AddAuthorization();

builder.Services.AddCors(o => o.AddPolicy(corsString,
  builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddBlogDatabase(builder.Configuration);
builder.Services.AddBlogProviders();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
  options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
  options.KnownNetworks.Clear();
  options.KnownProxies.Clear();
});

builder.Services.AddResponseCaching();
builder.Services.AddOutputCache(options =>
{
  options.AddPolicy(BlogifierConstant.OutputCacheExpire1, builder => builder.Expire(TimeSpan.FromMinutes(15)));
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
if (dbContext.Database.GetPendingMigrations().Any()) await dbContext.Database.MigrateAsync();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseWebAssemblyDebugging();
}
else
{
  app.UseExceptionHandler("/Error");
}
app.UseSerilogRequestLogging();
app.UseForwardedHeaders();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseCors(corsString);
app.UseRouting();
app.UseResponseCaching();
app.UseOutputCache();
app.UseAuthentication();
app.UseAuthorization();
var fileProviderRoot = Path.Combine(app.Environment.ContentRootPath, "App_Data/public");
if (!Directory.Exists(fileProviderRoot)) Directory.CreateDirectory(fileProviderRoot);
app.UseStaticFiles(new StaticFileOptions
{
  FileProvider = new PhysicalFileProvider(fileProviderRoot),
  RequestPath = "/data"
});
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapFallbackToFile("admin/{*path:nonfile}", "index.html");
app.MapFallbackToFile("account/{*path:nonfile}", "index.html");

await app.RunAsync();
