using Blogifier.Blogs;

namespace Blogifier.Models;

public class IndexModel : PagerModel
{
  public IndexModel(string absoluteUrl, BlogData blogData, int page) : base(absoluteUrl, blogData, page)
  {

  }
}
