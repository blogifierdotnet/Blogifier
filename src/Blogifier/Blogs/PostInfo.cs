using Blogifier.Shared;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Blogifier.Identity;

namespace Blogifier.Blogs;

public class PostInfo
{
  [Key]
  public int Id { get; set; }
  public int UserId { get; set; }
  public UserInfo User { get; set; } = default!;
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public DateTime CreatedAt { get; set; }
  [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
  public DateTime UpdatedAt { get; set; }
  [Required]
  [StringLength(160)]
  public string Title { get; set; } = default!;
  [Required]
  [StringLength(160)]
  public string Slug { get; set; } = default!;
  [Required]
  [StringLength(450)]
  public string Description { get; set; } = default!;
  [Required]
  public string Content { get; set; } = default!;
  [StringLength(160)]
  public string? Cover { get; set; }
  public int Views { get; set; }
  public double Rating { get; set; }
  public PostType Type { get; set; }
  public PostState State { get; set; }
  public DateTime PublishedAt { get; set; }
  public bool IsFeatured { get; set; }
}
