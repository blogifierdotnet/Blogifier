using System.ComponentModel.DataAnnotations;

namespace Blogifier.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Author name")]
        public string AuthorName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Set this user as administrator?")]
        public bool IsAdmin { get; set; }

        [Display(Name = "SendGrid API Key")]
        public string SendGridApiKey { get; set; }
        [Display(Name = "Send email notification?")]
        public bool SendEmailNotification { get; set; }
    }
}