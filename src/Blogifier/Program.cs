using Blogifier;
using Blogifier.Blogs;
using Blogifier.Data;
using Blogifier.Identity;
using Blogifier.Options;
using Blogifier.Providers;
using Blogifier.Shared.Resources;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, builder) => builder.ReadFrom.Configuration(context.Configuration).Enrich.FromLogContext());
builder.Services.Configure<BlogifierConstant>(builder.Configuration.GetSection(BlogifierConstant.OptionsName));
var redis = builder.Configuration.GetSection("Blogifier:Redis").Value;
if (redis == null) builder.Services.AddDistributedMemoryCache();
else builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = redis; options.InstanceName = "blogifier:"; });

builder.Services.AddHttpClient();
builder.Services.AddLocalization();
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
  .AddSignInManager<SignInManager>()
  .AddEntityFrameworkStores<AppDbContext>()
  .AddDefaultTokenProviders()
  .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory>();

builder.Services.AddAuthentication(options => options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme)
  .AddCookie(options =>
  {
    options.AccessDeniedPath = "/account/denied";
    options.LoginPath = "/account/login";
  });

builder.Services.AddAuthorization();
builder.Services.AddCors(o => o.AddPolicy(BlogifierConstant.PolicyCorsName,
  builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var section = builder.Configuration.GetSection("Blogifier");
var conn = section.GetValue<string>("ConnString");
if (section.GetValue<string>("DbProvider") == "SQLite")
  builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlite(conn));
else if (section.GetValue<string>("DbProvider") == "SqlServer")
  builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(conn));
else if (section.GetValue<string>("DbProvider") == "Postgres")
  builder.Services.AddDbContext<AppDbContext>(o => o.UseNpgsql(conn));
else if (section.GetValue<string>("DbProvider") == "MySql")
  builder.Services.AddDbContext<AppDbContext>(o => o.UseMySql(conn, ServerVersion.AutoDetect(conn)));

if (builder.Environment.IsDevelopment())
  builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddSingleton<MinioProvider>();
builder.Services.AddScoped<StorageProvider>();
builder.Services.AddScoped<AuthorProvider>();
builder.Services.AddScoped<BlogProvider>();
builder.Services.AddScoped<PostProvider>();
builder.Services.AddScoped<FeedProvider>();
builder.Services.AddScoped<CategoryProvider>();
builder.Services.AddScoped<AnalyticsProvider>();
builder.Services.AddScoped<NewsletterProvider>();
builder.Services.AddScoped<EmailProvider>();
builder.Services.AddScoped<ThemeProvider>();
builder.Services.AddScoped<ImportProvider>();
builder.Services.AddScoped<AboutProvider>();
builder.Services.AddScoped<OptionStore>();
builder.Services.AddScoped<BlogManager>();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
  options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
  options.KnownNetworks.Clear();
  options.KnownProxies.Clear();
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddResponseCaching();
builder.Services.AddOutputCache(options =>
{
  options.AddPolicy(BlogifierConstant.OutputCacheExpire1, builder => builder.Expire(TimeSpan.FromMinutes(15)));
});

builder.Services.AddControllersWithViews()
  .AddDataAnnotationsLocalization(options => options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(Resource)));
builder.Services.AddRazorPages().AddViewLocalization();

builder.Services.AddAutoMapper(typeof(Program));

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
app.UseCors(BlogifierConstant.PolicyCorsName);
app.UseRouting();
app.UseResponseCaching();
app.UseOutputCache();
app.UseRequestLocalization();
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
