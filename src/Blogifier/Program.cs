using Blogifier.Core.Data;
using Blogifier.Core.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.IO;
using System.Linq;

var corsString = "BlogifierPolicy";
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, builder) =>
  builder.ReadFrom.Configuration(context.Configuration).Enrich.FromLogContext());

builder.Services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

builder.Services.AddAuthentication(options =>
  options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme)
  .AddCookie();

builder.Services.AddCors(o => o.AddPolicy(corsString,
  builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddBlogDatabase(builder.Configuration);

builder.Services.AddBlogProviders();

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
if (dbContext.Database.GetPendingMigrations().Any())
    await dbContext.Database.MigrateAsync();

if (app.Environment.IsDevelopment())
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
app.UseCors(corsString);
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "App_Data/public")),
    RequestPath = "/data"
});
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapFallbackToFile("admin/{*path:nonfile}", "index.html");
app.MapFallbackToFile("account/{*path:nonfile}", "index.html");

await app.RunAsync();
