using System.ComponentModel.DataAnnotations;

namespace Blogifier.Shared;

public class UserEditorDto
{
  public string Email { get; set; } = default!;
  public string UserName { get; set; } = default!;
  [DataType(DataType.Password)]
  public string? Password { get; set; } = default!;
  [DataType(DataType.Password)]
  [Compare("Password", ErrorMessage = "Passwords do not match")]
  public string? PasswordConfirm { get; set; } = default!;
  public string NickName { get; set; } = default!;
  public string? Avatar { get; set; }
  public string? Bio { get; set; }
  public UserType Type { get; set; }
}
