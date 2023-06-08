using System.ComponentModel.DataAnnotations;

namespace Blogifier.Shared;

public class AccountRegisterModel : AccountLoginModel
{
  [Required]
  public string UserName { get; set; } = default!;
  [Required]
  [StringLength(256)]
  public string NickName { get; set; } = default!;
  [Required]
  [DataType(DataType.Password)]
  [Compare("Password", ErrorMessage = "Passwords do not match")]
  public string PasswordConfirm { get; set; } = default!;
}
