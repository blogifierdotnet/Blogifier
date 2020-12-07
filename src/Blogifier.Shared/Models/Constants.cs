namespace Blogifier.Shared
{
	public class Constants
	{
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

      public static string DefaultAvatar = "admin/img/avatar.jpg";
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

      public static string IdentityCookieName = ".AspNetCore.Identity.Application";
   }
}
