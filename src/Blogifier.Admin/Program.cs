using Blogifier;
using Blogifier.Admin;
using Blogifier.Admin.Services;
using Blogifier.Identity;
using KristofferStrube.Blazor.FileAPI;
using KristofferStrube.Blazor.FileSystemAccess;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Sotsera.Blazor.Toaster.Core.Models;
using System;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddLocalization();
builder.Services.AddOptions();

builder.Services.AddAuthorizationCore(options =>
  options.AddPolicy(BlogifierConstant.PolicyAdminName, policy =>
    policy.RequireClaim(BlogifierClaimTypes.Type, BlogifierConstant.PolicyAdminValue)));

builder.Services.AddScoped(sp => new HttpClient(new HttpClientHandler { AllowAutoRedirect = false })
{
  BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});
builder.Services.AddScoped<AuthenticationStateProvider, BlogAuthStateProvider>();
builder.Services.AddToaster(config =>
{
  config.PositionClass = Defaults.Classes.Position.BottomRight;
  config.PreventDuplicates = true;
  config.NewestOnTop = false;
});
// Configure with custom script path
builder.Services.AddFileSystemAccessServiceInProcess(options =>
{
  // The file at this path in this example is manually copied to wwwroot folder
  // options.BasePath = "content/";
  // options.ScriptPath = $"custom-path/{FileSystemAccessOptions.DefaultNamespace}.js";
});
builder.Services.AddScoped<ToasterService>();
builder.Services.AddURLServiceInProcess();

await builder.Build().RunAsync();
