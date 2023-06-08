using System.ComponentModel.DataAnnotations;

namespace Blogifier.Shared;

public class SubscriberApplyDto
{
  [Required]
  [EmailAddress]
  public string Email { get; set; } = default!;
}
