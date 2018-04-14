using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Data
{
    public class BlogPost
    {
        public BlogPost() { }

        public int Id { get; set; }

        [StringLength(250)]
        public string UserId { get; set; }

        [Required]
        [StringLength(160)]
        public string Title { get; set; }

        [Required]
        [RegularExpression("^[a-z0-9-]+$", ErrorMessage = "Slug format not valid.")]
        [StringLength(160)]
        public string Slug { get; set; }

        [Required]
        [StringLength(450)]
        public string Description { get; set; }

        public string Content { get; set; }

        [StringLength(255)]
        public string Cover { get; set; }

        public int PostViews { get; set; }
        public double Rating { get; set; }

        public DateTime Published { get; set; }
    }
}