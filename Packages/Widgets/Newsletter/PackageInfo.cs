using Blogifier.Core.Data.Models;

public class PackageInfo : IPackageInfo
{
    public PackageListItem GetAttributes()
    {
        return new PackageListItem
        {
            Title = "Newsletter",
            Description = "Newsletter email subscription widget",
            IconUrl = "https://avatars0.githubusercontent.com/u/19671571?v=4&amp;s=180",
            ProjectUrl = "https://github.com/blogifierdotnet/Blogifier",
            Tags = "widget,newsletter,email"
        };
    }
}