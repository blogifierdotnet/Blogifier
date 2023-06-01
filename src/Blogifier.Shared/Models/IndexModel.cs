using Blogifier.Shared;
namespace Blogifier.Models;

public class IndexModel : PostPagerModel
{
  public IndexModel(PostPagerDto pager, MainDto main) : base(pager, main)
  {
  }
}
