﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Blogifier.Models
{
    public class Author
    {
        public Author() { }

        /// <summary>
        /// Author ID
        /// </summary>
        public int Id { get; set; }

        [StringLength(160)]
        public string AppUserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(160)]
        [Display(Name = "User name")]
        public string DisplayName { get; set; }

        [Display(Name = "User bio")]
        public string Bio { get; set; }

        [StringLength(160)]
        public string Avatar { get; set; }

        public bool IsAdmin { get; set; }
        public DateTime Created { get; set; }
    }
}
