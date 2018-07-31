using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Data
{
    public class AppItem
    {
        public string DbProvider { get; set; }
        public string ConnString { get; set; }
        [Required]
        [StringLength(160)]
        public string Title { get; set; }
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
        [StringLength(160)]
        [Display(Name = "Blog cover URL")]
        public string Cover { get; set; }
        [StringLength(160)]
        [Display(Name = "Blog logo URL")]
        public string Logo { get; set; }
        [StringLength(160)]
        [Display(Name = "Blog avatar")]
        public string Avatar { get; set; }
        [Required]
        [StringLength(120)]
        public string Theme { get; set; }
        public IList<SelectListItem> BlogThemes { get; set; }

        [Display(Name = "Items per page")]
        public int ItemsPerPage { get; set; }

        public bool UseDescInPostList { get; set; }

        public string ImportTypes { get; set; }
    }
}