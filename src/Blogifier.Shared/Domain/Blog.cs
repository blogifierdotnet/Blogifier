using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogifier.Shared;

public class Blog
{
  [Key]
  public int Id { get; set; }
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public DateTime CreatedAt { get; set; }
  [StringLength(160)]
  public string Title { get; set; } = default!;
  [StringLength(450)]
  public string Description { get; set; } = default!;
  [StringLength(160)]
  public string Theme { get; set; } = default!;
  public bool IncludeFeatured { get; set; }
  public int ItemsPerPage { get; set; }
  [StringLength(160)]
  public string? Cover { get; set; }
  [StringLength(160)]
  public string? Logo { get; set; } = default!;
  [StringLength(2000)]
  public string? HeaderScript { get; set; }
  [StringLength(2000)]
  public string? FooterScript { get; set; }
  public int AnalyticsListType { get; set; }
  public int AnalyticsPeriod { get; set; }
  public List<Post>? Posts { get; set; }
  public List<Author>? Authors { get; set; }
}
