using System.ComponentModel.DataAnnotations;

namespace Blogifier.Shared
{
    public class UnsubscribeRequest
    {
        [Required]
        public string Token { get; set; }
    }
}