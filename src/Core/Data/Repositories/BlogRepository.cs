using System.Linq;
using System.Threading.Tasks;

namespace Core.Data
{
    public interface IBlogRepository : IRepository<Blog>
    {
        Task SaveBlog(BlogItem item);
        Task<BlogItem> GetBlog();
    }

    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        AppDbContext _db;

        public BlogRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<BlogItem> GetBlog()
        {
            var blog = _db.Blogs.FirstOrDefault();
            return await Task.FromResult(MapBlogToItem(blog));
        }

        public async Task SaveBlog(BlogItem item)
        {
            var blog = _db.Blogs.FirstOrDefault();

            if (blog == null)
            {
                _db.Blogs.Add(new Blog 
                { 
                    Title = item.Title,
                    Description = item.Description,
                    Logo = item.Logo,
                    Cover = item.Cover,
                    Theme = item.Theme,
                    PostListType = item.PostListType,
                    ItemsPerPage = item.ItemsPerPage 
                });
            }
            else
            {
                blog.Title = item.Title;
                blog.Description = item.Description;
                blog.Logo = item.Logo;
                blog.Cover = item.Cover;
                blog.Theme = item.Theme;
                blog.PostListType = item.PostListType;
                blog.ItemsPerPage = item.ItemsPerPage;
            }
            await _db.SaveChangesAsync();
        }

        BlogItem MapBlogToItem(Blog blog)
        {
            return new BlogItem
            {
                Title = blog.Title,
                Description = blog.Description,
                Logo = blog.Logo,
                Cover = blog.Cover,
                Theme = blog.Theme,
                PostListType = blog.PostListType,
                ItemsPerPage = blog.ItemsPerPage 
            };
        }
    }
}