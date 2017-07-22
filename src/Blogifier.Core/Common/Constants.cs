namespace Blogifier.Core.Common
{
    public class Constants
    {
        // default value is "blog/" for blogifier to use "site.com/blog"
        // if empty string, blog takes over the application
        // so instead of "site.com/blog" blogifier will be using "site.com" 
        public const string BlogRoute = "";

        public const string ProfileNotFound = "Profile not found";
        public const string ItemSaved = "Item saved";
    }
}
