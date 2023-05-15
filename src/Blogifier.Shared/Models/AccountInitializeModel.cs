using System.ComponentModel.DataAnnotations;

namespace Blogifier.Shared;

public class AccountInitializeModel : AccountRegisterModel
{
  [Required]
  public string Title { get; set; } = default!;
  [Required]
  public string Description { get; set; } = default!;
}
