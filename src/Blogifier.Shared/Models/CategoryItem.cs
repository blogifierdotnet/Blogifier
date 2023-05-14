using System;

namespace Blogifier.Shared;

public class CategoryItem
{
  public bool Selected { get; set; }
  public int Id { get; set; }
  public string Category { get; set; } = default!;
  public string? Description { get; set; }
  public int PostCount { get; set; }
  public DateTime DateCreated { get; set; }
}
