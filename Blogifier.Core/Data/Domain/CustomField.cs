using System.ComponentModel.DataAnnotations;

namespace Blogifier.Core.Data.Domain
{
    public class CustomField : BaseEntity
    {
        public CustomType CustomType { get; set; }
        public int ParentId { get; set; }

        [Required]
        [StringLength(160)]
        public string Title { get; set; }
        [Required]
        [StringLength(140)]
        public string CustomKey { get; set; }     
        public string CustomValue { get; set; }
    }

    public enum CustomType
    {
        Profile = 0,
        Post = 1,
        Asset = 2,
        Application = 3
    }
}
