using Blogifier.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Blogifier.Models;

public class PostInfoModel
{
  public int Id { get; set; }
  public PostType PostType { get; set; }
  [Required]
  public string Title { get; set; }
  public string Slug { get; set; }
  public string Description { get; set; }
  [Required]
  public string Content { get; set; }
  public ICollection<Category> Categories { get; set; }
  public string Cover { get; set; }
  public int PostViews { get; set; }
  public double Rating { get; set; }
  public DateTime Published { get; set; }
  public bool IsPublished { get { return Published > DateTime.MinValue; } }
  public bool Featured { get; set; }
  public Author Author { get; set; }
  public SaveStatus Status { get; set; }
  public List<SocialField> SocialFields { get; set; }
  public bool Selected { get; set; }
}
