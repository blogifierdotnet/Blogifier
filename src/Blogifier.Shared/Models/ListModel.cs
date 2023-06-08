using System.Collections.Generic;

namespace Blogifier.Shared;

public class ListModel
{
  public BlogItem Blog { get; set; } = default!;
  public Author Author { get; set; } = default!; // posts by author
  public string Category { get; set; } = default!; // posts by category
  public IEnumerable<PostItem> Posts { get; set; } = default!;
  public Pager Pager { get; set; } = default!; 
  public PostListType PostListType { get; set; } = default!;
}
