using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogifier.Shared;

public class Newsletter
{
  [Key]
  public int Id { get; set; }
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public DateTime CreatedAt { get; set; }
  [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
  public DateTime UpdatedAt { get; set; }
  public int PostId { get; set; }
  public bool Success { get; set; }
  public Post? Post { get; set; }
}
