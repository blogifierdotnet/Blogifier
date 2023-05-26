using Blogifier.Shared;

namespace Blogifier.Blogs;

public class PostSearch
{
  public PostSearch(Post post, int rank)
  {
    Post = post;
    Rank = rank;
  }
  public Post Post { get; set; }
  public int Rank { get; set; }
}
