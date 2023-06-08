using Blogifier.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blogifier.Shared;

public class Category : AppEntity<int>
{
  public DateTime CreatedAt { get; set; }
  [StringLength(120)]
  public string Content { get; set; } = default!;
  [StringLength(255)]
  public string? Description { get; set; } = default!;
  public List<PostCategory>? PostCategories { get; set; }
}
