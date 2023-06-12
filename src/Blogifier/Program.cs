using Blogifier;
using Blogifier.Blogs;
using Blogifier.Caches;
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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, builder) =>
{
  builder.ReadFrom
    .Configuration(context.Configuration)
    .Enrich
    .FromLogContext();
});

builder.Services.Configure<BlogifierConfigure>(builder.Configuration.GetSection(BlogifierConstant.Key));

builder.Services.AddHttpClient();
builder.Services.AddLocalization();

builder.Services.AddDbContext(builder.Environment, builder.Configuration);
builder.Services.AddCache(builder.Environment, builder.Configuration);

builder.Services.AddIdentity();
builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
  .AddIdentityCookies();
builder.Services.AddAuthorization();

builder.Services.AddSingleton<StorageMinioProvider>();

builder.Services.AddScoped<MarkdigProvider>();
builder.Services.AddScoped<ReverseProvider>();

builder.Services.AddSingleton<ImportRssProvider>();

builder.Services.AddScoped<UserProvider>();
builder.Services.AddScoped<PostProvider>();
builder.Services.AddScoped<CategoryProvider>();
builder.Services.AddScoped<NewsletterProvider>();
builder.Services.AddScoped<SubscriberProvider>();
builder.Services.AddScoped<StorageLocalProvider>();
builder.Services.AddScoped<OptionProvider>();
builder.Services.AddScoped<AnalyticsProvider>();

builder.Services.AddScoped<StorageProvider>();
builder.Services.AddScoped<EmailManager>();
builder.Services.AddScoped<ImportManager>();
builder.Services.AddScoped<PostManager>();
builder.Services.AddScoped<BlogManager>();
builder.Services.AddScoped<MainMamager>();

builder.Services.AddCors(option =>
{
  option.AddPolicy(BlogifierConstant.PolicyCorsName, builder =>
  {
    builder.AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader();
  });
});
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
  options.AddPolicy(BlogifierConstant.OutputCacheExpire1, builder =>
    builder.Expire(TimeSpan.FromMinutes(15)));
});

builder.Services.AddControllersWithViews()
  .AddDataAnnotationsLocalization(options =>
    options.DataAnnotationLocalizerProvider =
      (type, factory) => factory.Create(typeof(Resource)));
builder.Services.AddRazorPages().AddViewLocalization();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

await app.RunDbContextMigrateAsync();

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
app.UseStorageStaticFiles();
app.UseCookiePolicy();
app.UseRouting();
app.UseRequestLocalization(new RequestLocalizationOptions()
  .AddSupportedCultures(BlogifierConstant.SupportedCultures)
  .AddSupportedUICultures(BlogifierConstant.SupportedCultures));
app.UseCors(BlogifierConstant.PolicyCorsName);
app.UseAuthentication();
app.UseAuthorization();
app.UseResponseCaching();
app.UseOutputCache();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapFallbackToFile("admin/{*path:nonfile}", "index.html");

await app.RunAsync();
