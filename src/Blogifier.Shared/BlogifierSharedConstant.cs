using Blogifier.Shared;
using System.Text.Json;

namespace Blogifier;

public static class BlogifierSharedConstant
{
  public const string PolicyAdminName = "Administrator";
  public static readonly string PolicyAdminValue = $"{((int)UserType.Administrator)}";
  public static string DefaultAvatar = "img/avatar.webp";
  public static string DefaultCover = "img/cover.jpg";
  public static string DefaultLogo = "img/logo-sm.png";
  public static readonly JsonSerializerOptions DefaultJsonSerializerOptionss = new(JsonSerializerDefaults.Web);
}
