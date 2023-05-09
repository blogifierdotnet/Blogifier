using Blogifier.Blogs;
using System.Collections.Generic;

namespace Blogifier.Models;

public class IndexModel : PostsModel
{
  public IndexModel(string absoluteUrl, BlogData blogData, int page, IEnumerable<PostInfo> posts) : base(absoluteUrl, blogData, page, posts)
  {

  }
}
