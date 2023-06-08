using Blogifier.Data;
using Blogifier.Shared;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogifier.Newsletters;

public class Newsletter : AppEntity<int>
{
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public DateTime CreatedAt { get; set; }
  [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
  public DateTime UpdatedAt { get; set; }
  public int PostId { get; set; }
  public bool Success { get; set; }
  public Post Post { get; set; } = default!;
}
