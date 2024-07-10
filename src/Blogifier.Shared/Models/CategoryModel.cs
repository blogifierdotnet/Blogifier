namespace Blogifier.Shared;

public class CategoryModel(string category, PostPagerDto pager, MainDto main) : PostPagerModel(pager, main)
{
  public string Category { get; set; } = category;
}
