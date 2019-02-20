using System.ComponentModel.DataAnnotations;

namespace Core.Data
{
    public class AppItem
    {
        public string Avatar { get; set; }
        public bool DemoMode { get; set; }
        public string ImageExtensions { get; set; }
        public string ImportTypes { get; set; }
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
    }
}