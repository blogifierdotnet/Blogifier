using System.ComponentModel.DataAnnotations;

namespace Blogifier.Shared;

public class LoginModel
{
    [Required] [EmailAddress] public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Captcha must be completed.", AllowEmptyStrings = false)]
    public string CaptchaResponse { get; set; }
}
