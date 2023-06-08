using Blogifier.Blogs;
using Blogifier.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogifier.Shared;

public class Post
{
  [Key]
  public int Id { get; set; }

  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public DateTime CreatedAt { get; set; }
  [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
  public DateTime UpdatedAt { get; set; }
  public int AuthorId { get; set; }
  public int UserId { get; set; }
  public UserInfo User { get; set; } = default!;
  [Required]
  [StringLength(160)]
  public string Title { get; set; } = default!;
  [Required]
  [StringLength(160)]
  public string Slug { get; set; } = default!;
  [Required]
  [StringLength(450)]
  public string Description { get; set; } = default!;
  public string Content { get; set; } = default!;
  [StringLength(160)]
  public string? Cover { get; set; }
  public int Views { get; set; }
  public double Rating { get; set; }
  public bool IsFeatured { get; set; }
  public DateTime PublishedAt { get; set; }
  public PostType PostType { get; set; }
  public PostState State { get; set; }
  public List<PostCategory>? PostCategories { get; set; }
  public bool Selected { get; set; }
}
