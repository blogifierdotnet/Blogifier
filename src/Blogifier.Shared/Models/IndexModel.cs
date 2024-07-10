using Blogifier.Shared;
namespace Blogifier.Models;

public class IndexModel(PostPagerDto pager, MainDto main) : PostPagerModel(pager, main)
{
}
