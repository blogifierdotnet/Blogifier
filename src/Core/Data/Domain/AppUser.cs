using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Data
{
    public class AppUser : IdentityUser
    {
        public bool IsAdmin { get; set; }
        [StringLength(160)]
        public string DisplayName { get; set; }
        [StringLength(160)]
        public string Avatar { get; set; }
        public DateTime Created { get; set; }
    }
}