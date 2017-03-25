using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blogifier.Core.Data.Domain
{
    public class Publication : BaseEntity
    {
        public Publication() { }

        public int PublisherId { get; set; }

        [Required]
        [RegularExpression("^[a-z0-9-]+$", ErrorMessage = "Slug format not valid.")]
        [StringLength(160)]
        public string Slug { get; set; }
        [Required]
        [StringLength(160)]
        public string Title { get; set; }
        [Required]
        [StringLength(450)]
        public string Description { get; set; }
        [Required]
        public string Content { get; set; }
        [StringLength(160)]
        public string Image { get; set; }
        
        public int PostViews { get; set; }

        public DateTime Published { get; set; }

        public Publisher Publisher { get; set; }
        public List<PublicationCategory> PublicationCategories { get; set; }
    }
}