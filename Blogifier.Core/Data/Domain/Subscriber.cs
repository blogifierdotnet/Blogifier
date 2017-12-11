using System;
using System.ComponentModel.DataAnnotations;

namespace Blogifier.Core.Data.Domain
{
    public class Subscriber : BaseEntity
    {
        [Required]
        [EmailAddress]
        [StringLength(160)]
        public string Email { get; set; }

        public string Browser { get; set; }
        public string Device { get; set; }
        public string Os { get; set; }
        public string Ip { get; set; }
        public DateTime Created { get; set; }
        public bool Active { get; set; }
    }
}