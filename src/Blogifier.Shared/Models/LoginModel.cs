using System.ComponentModel.DataAnnotations;

namespace Blogifier.Shared;

public class LoginModel
{
  [Required]
  [EmailAddress]
  public string Email { get; set; } = default!;
  [Required]
  [DataType(DataType.Password)]
  public string Password { get; set; } = default!;
}
