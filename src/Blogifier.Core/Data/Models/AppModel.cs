using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blogifier.Core.Data
{
    public class AppItem
    {
        public string Avatar { get; set; }
        public bool DemoMode { get; set; }
        public string ImageExtensions { get; set; } = "png,jpg,jpeg,gif,bmp,tiff";
        public string ImportTypes { get; set; } = "zip,7z,xml,pdf,doc,docx,xls,xlsx,mp3,avi";
        public bool SeedData { get; set; }
    }

    public class BlogItem
    {
        [Required]
        [StringLength(160)]
        public string Title { get; set; } = "Blog Title";
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
        [Display(Name = "Items per page")]
        public int ItemsPerPage { get; set; }
        [StringLength(160)]
        [Display(Name = "Blog cover URL")]
        public string Cover { get; set; }
        [StringLength(160)]
        [Display(Name = "Blog logo URL")]
        public string Logo { get; set; }
        [Required]
        [StringLength(120)]
        public string Theme { get; set; }
        [Required]
        [StringLength(15)]
        public string Culture { get; set; }
        public bool IncludeFeatured { get; set; }
        public List<SocialField> SocialFields { get; set; }

        public string HeaderScript { get; set; }
        public string FooterScript { get; set; }
    }
}