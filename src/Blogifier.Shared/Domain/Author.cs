using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogifier.Shared;

public class Author
{
  [Key]
  public int Id { get; set; }
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public DateTime CreatedAt { get; set; }
  [EmailAddress]
  [StringLength(160)]
  public string Email { get; set; } = default!;
  [StringLength(160)]
  public string Password { get; set; } = default!;
  [StringLength(160)]
  public string DisplayName { get; set; } = default!;
  [StringLength(2000)]
  public string? Bio { get; set; }
  [StringLength(400)]
  public string? Avatar { get; set; }
  public bool IsAdmin { get; set; }
}
