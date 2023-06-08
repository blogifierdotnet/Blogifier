namespace Blogifier.Shared;

public class PostPagerModel : MainModel
{
  public PostPagerModel(PostPagerDto pager, MainDto main) : base(main)
  {
    Pager = pager;
  }
  public PostPagerDto Pager { get; }
}
