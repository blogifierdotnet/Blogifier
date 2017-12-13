using Blogifier.Core.Data.Models;

public class PackageInfo : IPackageInfo
{
    public PackageListItem GetAttributes()
    {
        return new PackageListItem
        {
            Title = "OneSix",
            Version = "1.0.0",
            Description = "OneSix theme for Blogifier blogs.",
            Icon = "https://avatars0.githubusercontent.com/u/19671571?v=4&amp;s=180",
            Author = "Blogifier",
            ProjectUrl = "https://github.com/blogifierdotnet/Blogifier",
            Tags = "theme,onesix"
        };
    }
}