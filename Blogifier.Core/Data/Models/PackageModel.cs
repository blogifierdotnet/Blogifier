using Blogifier.Core.Common;
using System;
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
        public PackageListItem PackageItem { get; set; }
        public dynamic Settings { get; set; }
    }

    public class PackageListItem
    {
        public PackageType PkgType { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string Cover { get; set; }
        public string Author { get; set; }
        public string ProjectUrl { get; set; }
        public string Tags { get; set; }

        // for app store
        public int Downloads { get; set; }
        public double Rating { get; set; }

        public string Version { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool HasSettings { get; set; }
        public bool Enabled { get; set; }
    }

    public class ThemeSettingsModel : AdminBaseModel
    {
        public List<ZoneViewModel> Zones { get; set; }

    }

    public class ZoneViewModel
    {
        public string Theme { get; set; }
        public string Zone { get; set; }
        public List<string> Widgets { get; set; }
    }

    public enum PackageType
    {
        Undefined,
        Widgets,
        Themes,
        Plugins
    }
}