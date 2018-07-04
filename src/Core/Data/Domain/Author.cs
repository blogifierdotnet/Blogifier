using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Data
{
    public class Author
    {
        public Author() { }

        public int Id { get; set; }

        [StringLength(160)]
        public string AppUserId { get; set; }
        [StringLength(160)]
        public string AppUserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(160)]
        [Display(Name = "User name")]
        public string DisplayName { get; set; }

        [StringLength(160)]
        public string Avatar { get; set; }

        public bool IsAdmin { get; set; }
        public DateTime Created { get; set; }
    }

    //public class AuthorItem
    //{
    //    [Required]
    //    public string Id { get; set; }
    //    [Required]
    //    [Display(Name = "User name")]
    //    public string UserName { get; set; }
    //    [Required]
    //    [EmailAddress]
    //    public string Email { get; set; }
    //    [Required]
    //    [Display(Name = "Display name")]
    //    public string DisplayName { get; set; }
    //    [Display(Name = "Avatar image")]
    //    public string Avatar { get; set; }
    //    public DateTime Created { get; set; }
    //    public bool IsAdmin { get; set; }
    //}
}