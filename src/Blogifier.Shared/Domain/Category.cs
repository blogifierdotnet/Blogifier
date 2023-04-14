using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogifier.Shared
{
    public class Category
    {
        public Category() { }
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Content { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateCreated { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateUpdated { get; set; }
        public List<PostCategory> PostCategories { get; set; }
    }
}
