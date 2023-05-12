namespace Blogifier.Shared;

public class SearchResult
{
  public int Rank { get; set; }
  public PostItemDto Item { get; set; } = default!;
}
