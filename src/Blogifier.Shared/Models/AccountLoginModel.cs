using System.ComponentModel.DataAnnotations;

namespace Blogifier.Shared;

public class AccountLoginModel : AccountModel
{
  public bool ShowError { get; set; }
  [Required]
  [EmailAddress]
  public string Email { get; set; } = default!;
  [Required]
  [DataType(DataType.Password)]
  public string Password { get; set; } = default!;
}
