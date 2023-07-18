using Blogifier.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Blogifier.Identity;

public static class IdentityExtensions
{
  public static IServiceCollection AddIdentity(this IServiceCollection services)
  {
    services.AddScoped<UserClaimsPrincipalFactory>();
    services.AddIdentityCore<UserInfo>(options =>
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
    services.ConfigureApplicationCookie(options =>
    {
      options.AccessDeniedPath = "/account/accessdenied";
      options.LoginPath = "/account/login";
    });
    return services;
  }
}
