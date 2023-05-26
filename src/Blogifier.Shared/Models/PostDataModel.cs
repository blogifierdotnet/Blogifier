namespace Blogifier.Shared;

public class PostDataModel : MainModel
{
  public PostDataModel(PostSlugDto postSlug, string categoriesUrl, MainDto main) : base(main)
  {
    PostSlug = postSlug;
    CategoriesUrl = categoriesUrl;
  }
  public PostSlugDto PostSlug { get; set; }
  public string CategoriesUrl { get; set; }
}
