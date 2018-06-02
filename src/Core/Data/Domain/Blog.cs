using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Core.Data
{
    public class Blog
    {
        public int Id { get; set; }
        [Required]
        [StringLength(160)]
        public string Title { get; set; }
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
        [StringLength(160)]
        public string Cover { get; set; }
        [StringLength(160)]
        public string Logo { get; set; }
        [Required]
        [StringLength(120)]
        public string Theme { get; set; }
        public string PostListType { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
