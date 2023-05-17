using Blogifier.Admin;
using Blogifier.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Sotsera.Blazor.Toaster.Core.Models;
using System;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.Services.AddLocalization();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore(options =>
{
  options.AddPolicy(BlogifierClaimTypes.AuthorityAdmin,
    policy => policy.RequireClaim(BlogifierClaimTypes.AuthorityAdmin, BlogifierClaimTypes.AuthorityYes));
});
builder.Services.AddHttpClient(string.Empty, client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddScoped<AuthenticationStateProvider, BlogAuthStateProvider>();
builder.Services.AddToaster(config =>
{
  config.PositionClass = Defaults.Classes.Position.BottomRight;
  config.PreventDuplicates = true;
  config.NewestOnTop = false;
});
builder.Build().RunAsync();
