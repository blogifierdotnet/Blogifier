using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogifier.Shared
{
    public class MailSetting
    {
        public int Id { get; set; }
        [Required]
        [StringLength(160)]
        public string Host { get; set; }
        public int Port { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(120)]
        public string UserEmail { get; set; }
        [Required]
        [StringLength(120)]
        public string UserPassword { get; set; }
        [Required]
        [StringLength(120)]
        public string FromName { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(120)]
        public string FromEmail { get; set; }
        [Required]
        [StringLength(120)]
        public string ToName { get; set; }
        public bool Enabled { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateCreated { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateUpdated { get; set; }
        public Blog Blog { get; set; }
    }
}
