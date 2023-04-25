using System.ComponentModel.DataAnnotations;

namespace Blogifier.Shared;

public class ImportRssModel
{
  [Required]
  [Url]
  public string FeedUrl { get; set; } = default!;
}
