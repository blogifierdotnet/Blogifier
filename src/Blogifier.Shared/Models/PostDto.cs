using Blogifier.Blogs;
using Blogifier.Shared;
using System;
using System.Collections.Generic;

namespace Blogifier.Models;

public class PostDto
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public int AuthorId { get; set; }
  public int UserId { get; set; }
  public string Title { get; set; } = default!;
  public string Slug { get; set; } = default!;
  public string Description { get; set; } = default!;
  public string Content { get; set; } = default!;
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
