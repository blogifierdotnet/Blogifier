using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogifier.Shared;

public class Subscriber
{
  [Key]
  public int Id { get; set; }
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public DateTime CreatedAt { get; set; }
  [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
  public DateTime UpdatedAt { get; set; }
  [EmailAddress]
  [StringLength(160)]
  public string Email { get; set; } = default!;
  [StringLength(80)]
  public string? Ip { get; set; }
  [StringLength(120)]
  public string? Country { get; set; }
  [StringLength(120)]
  public string? Region { get; set; }
  public Blog? Blog { get; set; }
}
