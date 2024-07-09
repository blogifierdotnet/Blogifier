using Blogifier.Shared;

namespace Blogifier.Posts;

public class PostSearch(PostItemDto post, int rank)
{
  public PostItemDto Post { get; set; } = post;
  public int Rank { get; set; } = rank;
}
