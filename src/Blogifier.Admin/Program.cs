using Blogifier;
using Blogifier.Admin;
using Blogifier.Admin.Interop;
using Blogifier.Admin.Services;
using Blogifier.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Sotsera.Blazor.Toaster.Core.Models;
using System;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.Services.AddLocalization();
builder.Services.AddOptions();

builder.Services.AddAuthorizationCore(options =>
  options.AddPolicy(BlogifierSharedConstant.PolicyAdminName, policy =>
    policy.RequireClaim(BlogifierClaimTypes.Type, BlogifierSharedConstant.PolicyAdminValue)));

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
builder.Services.AddScoped<ToasterService>();
builder.Services.AddScoped<EditorJsInterop>();
builder.Services.AddScoped<CommonJsInterop>();
await builder.Build().RunAsync();
