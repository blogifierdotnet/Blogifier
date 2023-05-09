using System.ComponentModel.DataAnnotations;

namespace Blogifier.Models;

public class AuthorModel
{
  public string Nickname { get; set; } = default!;
  public string? Avatar { get; set; }
  public string? Bio { get; set; }
  public string? Gender { get; set; }
}
