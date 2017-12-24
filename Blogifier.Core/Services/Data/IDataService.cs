using Blogifier.Core.Data.Models;
using System.Threading.Tasks;

namespace Blogifier.Core.Services.Data
{
    public interface IDataService
    {
        BlogPostsModel GetPosts(int page, bool pub = false);
        Task<BlogAuthorModel> GetPostsByAuthor(string auth, int page, bool pub = false);
        Task<BlogCategoryModel> GetPostsByCategory(string auth, string cat, int page, bool pub = false);
        Task<BlogCategoryModel> GetAllPostsByCategory(string cat, int page, bool pub = false);
        Task<BlogPostDetailModel> GetPostBySlug(string slug, bool pub = false);
        BlogPostsModel SearchPosts(string term, int page, bool pub = false);
    }
}