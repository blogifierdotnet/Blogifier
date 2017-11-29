using Blogifier.Core.Data.Models;

public class PackageInfo : IPackageInfo
{
    public PackageListItem GetAttributes()
    {
        return new PackageListItem
        {
            Title = "PostList",
            Description = "PostList widget to display latest, featured or popular posts",
            Icon = "https://avatars0.githubusercontent.com/u/19671571?v=4&amp;s=180",
            ProjectUrl = "https://github.com/blogifierdotnet/Blogifier",
            Tags = "widget,postlist,featured,latest,popular"
        };
    }
}