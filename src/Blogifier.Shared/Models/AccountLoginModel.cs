using System.ComponentModel.DataAnnotations;

namespace Blogifier.Shared;

public class AccountLoginModel : AccountModel
{
  public bool ShowError { get; set; }
  public string Theme { get; set; } = default!;
  [Required]
  [EmailAddress]
  public string Email { get; set; } = default!;
  [Required]
  [DataType(DataType.Password)]
  public string Password { get; set; } = default!;
}
