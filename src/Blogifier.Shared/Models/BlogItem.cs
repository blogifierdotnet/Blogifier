using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Blogifier.Shared
{
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

      [JsonIgnore]
      public dynamic values { get; set; }
   }
}
