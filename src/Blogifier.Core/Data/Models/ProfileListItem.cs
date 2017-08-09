using System;

namespace Blogifier.Core.Data.Models
{
    public class ProfileListItem
    {
        public int ProfileId { get; set; }
        public string IdentityName { get; set; }
        public string AuthorName { get; set; }
        public bool IsAdmin { get; set; }

        public string Title { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }

        public int PostCount { get; set; }
        public int PostViews { get; set; }
        public long DbUsage { get; set; }

        public int AssetCount { get; set; }
        public int DownloadCount { get; set; }
        public long DiskUsage { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}