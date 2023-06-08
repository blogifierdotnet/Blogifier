using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogifier.Shared;

public class Category
{
  [Key]
  public int Id { get; set; }
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public DateTime CreatedAt { get; set; }
  [StringLength(120)]
  public string Content { get; set; } = default!;
  [StringLength(255)]
  public string? Description { get; set; } = default!;
  public List<PostCategory>? PostCategories { get; set; }
}
