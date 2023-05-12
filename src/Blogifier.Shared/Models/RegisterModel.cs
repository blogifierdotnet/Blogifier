using System.ComponentModel.DataAnnotations;

namespace Blogifier.Shared;

public class RegisterModel
{
  [Required]
  public string Name { get; set; } = default!;
  [Required]
  [EmailAddress]
  public string Email { get; set; } = default!;
  [Required]
  [DataType(DataType.Password)]
  public string Password { get; set; } = default!;
  [Required]
  [DataType(DataType.Password)]
  [Compare("Password", ErrorMessage = "Passwords do not match")]
  public string PasswordConfirm { get; set; } = default!;
}
