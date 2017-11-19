using Blogifier.Core.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blogifier.Core.Data.Models
{
    public interface IPackageInfo
    {
        PackageListItem GetAttributes();
    }

    public class AdminPackagesModel : AdminBaseModel
    {
        public IList<PackageListItem> Packages { get; set; }
        public Pager Pager { get; set; }
    }

    public class AdminSettingsModel : AdminBaseModel
    {
        public dynamic Settings { get; set; }
    }

    public class PackageListItem
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public string ProjectUrl { get; set; }
        public string RepositoryUrl { get; set; }
        public string Tags { get; set; }

        public string Version { get; set; }
        public bool HasSettings { get; set; }
        public bool Enabled { get; set; }
    }
}