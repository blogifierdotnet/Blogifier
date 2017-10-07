using Blogifier.Core.Data.Models;

namespace Blogifier.Core.Services.Data
{
    public interface IDataService
    {
        BlogPostsModel GetPosts(int page, bool pub = false);
        BlogAuthorModel GetPostsByAuthor(string auth, int page, bool pub = false);
        BlogCategoryModel GetPostsByCategory(string auth, string cat, int page, bool pub = false);
        BlogCategoryModel GetAllPostsByCategory(string cat, int page, bool pub = false);
        BlogPostDetailModel GetPostBySlug(string slug, bool pub = false);
        BlogPostsModel SearchPosts(string term, int page, bool pub = false);
    }
}