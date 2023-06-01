namespace Blogifier.Shared;

public class CategoryModel : PostPagerModel
{
  public string Category { get; set; }

  public CategoryModel(string category, PostPagerDto pager, MainDto main) : base(pager, main)
  {
    Category = category;
  }
}
