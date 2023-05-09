namespace Blogifier.Models;

using Blogifier.Blogs;
using System.Collections.Generic;
using System.Linq;

public class PostsModel : PagerModel
{
  public PostsModel(string absoluteUrl, BlogData blogData, int currentPage, IEnumerable<PostInfo> posts) : base(absoluteUrl, blogData, currentPage)
  {
    Posts = posts.Select(m => new PostInfoModel(m));
  }

  public IEnumerable<PostInfoModel> Posts { get; set; } = default!;
}
