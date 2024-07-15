namespace Blogifier.Shared;

public class PostModel(PostSlugDto postSlug, string categoriesUrl, MainDto main) : MainModel(main)
{
  public PostSlugDto PostSlug { get; set; } = postSlug;
  public string CategoriesUrl { get; set; } = categoriesUrl;
}
