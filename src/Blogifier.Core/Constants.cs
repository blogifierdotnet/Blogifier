namespace Blogifier.Core
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
    }

    public enum UploadType
    {
        Avatar,
        Attachement,
        AppLogo,
        AppCover,
        PostCover,
        PostImage
    }

    public enum AppFeatureFlags
    {
        DemoMode,
        EmailEnabled,
        ThumbnailsEnabled
    }
}