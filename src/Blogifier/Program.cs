using Blogifier;
using Blogifier.Blogs;
using Blogifier.Data;
using Blogifier.Identity;
using Blogifier.Newsletters;
using Blogifier.Options;
using Blogifier.Posts;
using Blogifier.Shared.Resources;
using Blogifier.Storages;
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
var redis = builder.Configuration.GetSection("Blogifier:Redis").Value;
if (redis == null) builder.Services.AddDistributedMemoryCache();
else builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = redis; options.InstanceName = "blogifier:"; });

builder.Services.AddHttpClient();
builder.Services.AddLocalization();
builder.Services.AddScoped<UserClaimsPrincipalFactory>();
builder.Services.AddIdentityCore<UserInfo>(options =>
{
  options.User.RequireUniqueEmail = true;
  options.Password.RequireUppercase = false;
  options.Password.RequireNonAlphanumeric = false;
  options.ClaimsIdentity.UserIdClaimType = BlogifierClaimTypes.UserId;
  options.ClaimsIdentity.UserNameClaimType = BlogifierClaimTypes.UserName;
  options.ClaimsIdentity.EmailClaimType = BlogifierClaimTypes.Email;
  options.ClaimsIdentity.SecurityStampClaimType = BlogifierClaimTypes.SecurityStamp;
}).AddUserManager<UserManager>()
  .AddSignInManager<SignInManager>()
  .AddEntityFrameworkStores<AppDbContext>()
  .AddDefaultTokenProviders()
  .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory>();

builder.Services.ConfigureApplicationCookie(options =>
{
  options.AccessDeniedPath = "/account/accessdenied";
  options.LoginPath = "/account/login";
});

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();

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
builder.Services.AddSingleton<MarkdigProvider>();
builder.Services.AddSingleton<ImportRssProvider>();

builder.Services.AddScoped<UserProvider>();
builder.Services.AddScoped<PostProvider>();
builder.Services.AddScoped<CategoryProvider>();
builder.Services.AddScoped<NewsletterProvider>();
builder.Services.AddScoped<SubscriberProvider>();
builder.Services.AddScoped<StorageProvider>();
builder.Services.AddScoped<OptionProvider>();
builder.Services.AddScoped<AnalyticsProvider>();

builder.Services.AddScoped<EmailManager>();
builder.Services.AddScoped<ImportManager>();
builder.Services.AddScoped<PostManager>();
builder.Services.AddScoped<BlogManager>();
builder.Services.AddScoped<MainMamager>();

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

using (var scope = app.Services.CreateScope())
{
  var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
  if (dbContext.Database.GetPendingMigrations().Any())
  {
    await dbContext.Database.MigrateAsync();
  }
}

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseWebAssemblyDebugging();
}
else
{
  app.UseExceptionHandler("/404");
}

app.UseForwardedHeaders();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
var fileProviderRoot = Path.Combine(app.Environment.ContentRootPath, "App_Data/public");
if (!Directory.Exists(fileProviderRoot)) Directory.CreateDirectory(fileProviderRoot);
app.UseStaticFiles(new StaticFileOptions
{
  FileProvider = new PhysicalFileProvider(fileProviderRoot),
  RequestPath = "/data"
});
app.UseCookiePolicy();
app.UseRouting();

var supportedCultures = new[] {
  "zh-CN",
  "zh-TW",
  "el-GR",
  "es",
  "fa",
  "pt-BR",
  "ru",
  "sv-SE",
  "ur-PK"
};
app.UseRequestLocalization(new RequestLocalizationOptions()
  .AddSupportedCultures(supportedCultures)
  .AddSupportedUICultures(supportedCultures));

app.UseCors(BlogifierConstant.PolicyCorsName);
app.UseAuthentication();
app.UseAuthorization();
app.UseResponseCaching();
app.UseOutputCache();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapFallbackToFile("admin/{*path:nonfile}", "index.html");

await app.RunAsync();
