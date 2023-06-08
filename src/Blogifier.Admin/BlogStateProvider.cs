using Blogifier.Shared;
using System;

namespace Blogifier.Admin;

public class BlogStateProvider
{
  public PostType PostType { get; set; } = PostType.Post;

  public event Action OnChange = default!;

  public void SetPostType(PostType postType)
  {
    PostType = postType;
    NotifyStateChanged();
  }

  private void NotifyStateChanged() => OnChange?.Invoke();
}
