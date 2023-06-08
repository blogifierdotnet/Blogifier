using System.ComponentModel.DataAnnotations;

namespace Blogifier.Shared;

public class ImportRssRequest
{
  [Required]
  [Url]
  public string FeedUrl { get; set; } = default!;
}
