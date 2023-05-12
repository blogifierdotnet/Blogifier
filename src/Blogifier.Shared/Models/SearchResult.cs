namespace Blogifier.Shared;

public class SearchResult
{
  public int Rank { get; set; }
  public PostItemModel Item { get; set; } = default!;
}
