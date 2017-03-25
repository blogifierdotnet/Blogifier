using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blogifier.Core.Data.Domain
{
    public class Publisher : BaseEntity
    {
        public Publisher() { }

        [Required]
        [StringLength(100)]
        public string IdentityName { get; set; }

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
        [StringLength(100)]
        public string AuthorName { get; set; }

        [Required]
        [StringLength(160)]
        [DataType(DataType.EmailAddress)]
        public string AuthorEmail { get; set; }

        [Required]
        [StringLength(160)]
        public string Theme { get; set; }
        [StringLength(160)]
        public string Logo { get; set; }
        [StringLength(160)]
        public string Avatar { get; set; }
        [StringLength(160)]
        public string Image { get; set; }

        public List<Publication> Publications { get; set; }
        public List<Asset> Assets { get; set; }
    }
}