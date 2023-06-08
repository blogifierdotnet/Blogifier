using System.ComponentModel.DataAnnotations;

namespace Blogifier.Shared;

public class ImportRssDto
{
  [Required][Url] public string FeedUrl { get; set; } = default!;
}
