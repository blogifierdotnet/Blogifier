namespace Blogifier.Shared;

public class PostPagerModel(PostPagerDto pager, MainDto main) : MainModel(main)
{
  public PostPagerDto Pager { get; } = pager;
}
