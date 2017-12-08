using Blogifier.Core.Data.Models;

public class PackageInfo : IPackageInfo
{
    public PackageListItem GetAttributes()
    {
        return new PackageListItem
        {
            Title = "Newsletter",
            Version = "1.0.0",
            Description = "Newsletter widget for Blogifier allows visitors to subscribe to new publications by email.",
            Icon = "https://avatars0.githubusercontent.com/u/19671571?v=4&amp;s=180",
            Author = "Blogifier",
            ProjectUrl = "https://github.com/blogifierdotnet/Blogifier",
            Tags = "widget,newsletter,email"
        };
    }
}