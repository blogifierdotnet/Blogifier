using Blogifier.Core.Data.Models;

public class PackageInfo : IPackageInfo
{
    public PackageListItem GetAttributes()
    {
        return new PackageListItem
        {
            Title = "Newsletter",
            Description = "Newsletter subscription widget",
            IconUrl = "https://avatars0.githubusercontent.com/u/19671571?v=4&amp;s=180",
            ProjectUrl = "http://blogifier.net",
            RepositoryUrl = "https://github.com/blogifierdotnet",
            Tags = "widget,newsletter,email"
        };
    }
}