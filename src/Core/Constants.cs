namespace Core
{
    public class Constants
    {
        public static string ConfigSectionKey = "Blogifier";
        public static string ConfigRepoKey = "GithubRepoUrl";

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

        public static string ThemeEditReturnUrl = "~/admin/settings/theme";
    }
}