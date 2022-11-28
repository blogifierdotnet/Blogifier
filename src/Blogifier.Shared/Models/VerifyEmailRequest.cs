using System.ComponentModel.DataAnnotations;

namespace Blogifier.Shared
{
    public class VerifyEmailRequest
    {
        [Required]
        public string Token { get; set; }
    }
}