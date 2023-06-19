using Blogifier.Shared;
using System.Text.Json;

namespace Blogifier;

public static class BlogifierSharedConstant
{
  public const string PolicyAdminName = "Administrator";
  public static readonly string PolicyAdminValue = $"{((int)UserType.Administrator)}";
  public static readonly string DefaultAvatar = "img/avatar.webp";
  public static readonly string DefaultCover = "img/cover.jpg";
  public static readonly string DefaultLogo = "img/logo-sm.png";
  public static readonly JsonSerializerOptions DefaultJsonSerializerOptionss = new(JsonSerializerDefaults.Web);
}
