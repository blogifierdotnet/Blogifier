using System.ComponentModel.DataAnnotations;

namespace Blogifier.Shared;

public class AccountProfileEditModel : AccountProfileModel
{
  public string? Error { get; set; }
  [Required]
  [EmailAddress]
  public string? Email { get; set; } = default!;
  [Required]
  public string NickName { get; set; } = default!;
  public string? Avatar { get; set; }
  public string? Bio { get; set; }
}
