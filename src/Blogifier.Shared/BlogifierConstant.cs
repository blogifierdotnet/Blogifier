using Blogifier.Shared;
using System.Text.Json;

namespace Blogifier;

public static class BlogifierConstant
{
  public const string PolicyAdminName = "Administrator";
  public static readonly string PolicyAdminValue = $"{((int)UserType.Administrator)}";

  public const string PolicyCorsName = "BlogifierPolicy";
  public const string OptionsName = "Blogifier";
  public const string StorageObjectUrl = "/storage/object";
  public const string OutputCacheExpire1 = "Expire1";

  public const string DefaultTitle = "Blog Title";
  public const string DefaultDescription = "Short Blog Description";
  public const string DefaultTheme = "standard";
  public const string DefaultVersion = "3.0";
  public static string DefaultAvatar = "img/avatar.webp";
  public static string DefaultCover = "img/cover.jpg";
  public static string DefaultLogo = "img/logo-sm.png";
  public const int DefaultItemsPerPage = 10;

  public static string ConfigSectionKey = "Blogifier";
  public static string ConfigRepoKey = "GithubRepoUrl";
  public static string ConfigNotificationsKey = "GithubNotificationsUrl";

  public static string NewestVersion = "last-version";
  public static string UpgradeDirectory = "_upgrade";

  // blog settings in custom fields
  public static string BlogTitle = "blog-title";
  public static string BlogDescription = "blog-description";
  public static string BlogItemsPerPage = "blog-items-per-page";
  public static string BlogTheme = "blog-theme";
  public static string BlogLogo = "blog-logo";
  public static string BlogCover = "blog-cover";
  public static string Culture = "culture";
  public static string IncludeFeatured = "blog-include-featured";
  public static string HeaderScript = "blog-header-script";
  public static string FooterScript = "blog-footer-script";

  public static string DummyEmail = "dummy@blog.com";

  public static string AvatarDataImage = "data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 250 250' width='250' height='250'%3E%3Crect width='250' height='250' fill='%23622aff'%3E%3C/rect%3E%3Ctext x='50%' y='53%' dominant-baseline='middle' text-anchor='middle' font-family='Arial, sans-serif' font-size='128px' fill='%23ffffff'%3E{0}%3C/text%3E%3C/svg%3E";
  public static string ImagePlaceholder = "admin/img/img-placeholder.png";
  public static string ThemeScreenshot = "screenshot.png";
  public static string ThemeEditReturnUrl = "~/admin/settings/theme";
  public static string ThemeDataFile = "data.json";

  // email model
  public static string EmailSelectedProvider = "email-selected-provider";
  public static string EmailFromName = "email-from-name";
  public static string EmailFromEmail = "email-from-email";
  public static string EmailSendgridApiKey = "email-sendgrid-api-key";
  public static string EmailSendgridConfigured = "email-sendgrid-configured";
  public static string EmailMailKitConfigured = "email-mailkit-configured";
  public static string EmailMailKitName = "email-mailkit-name";
  public static string EmailMailKitAddress = "email-mailkit-address";
  public static string EmailMailKitServer = "email-mailkit-server";
  public static string EmailMailKitPort = "email-mailkit-port";
  public static string EmailMailKitPassword = "email-mailkit-password";

  public static string PostDraft = "D";
  public static string PostFeatured = "F";
  public static string PostPublished = "P";
  public static string IdentityCookieName = ".AspNetCore.Identity.Application";
  public static readonly JsonSerializerOptions DefaultJsonSerializerOptionss = new(JsonSerializerDefaults.Web);

  public static class CacheKeys
  {
    public const string BlogData = "blogifier";
    public const string BlogMailData = $"{BlogData}:mail";
    public const string CategoryItemes = $"{BlogData}:category:itemes";
  }


  public static string CheckGetAvatarUrl(string? avatar)
  {
    if (!string.IsNullOrEmpty(avatar)) return avatar;
    return DefaultAvatar;
  }
}
