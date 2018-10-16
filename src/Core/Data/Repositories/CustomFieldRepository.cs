using System.Linq;
using System.Threading.Tasks;

namespace Core.Data
{
    public interface ICustomFieldRepository : IRepository<CustomField>
    {
        Task<BlogItem> GetBlogSettings();
        Task SaveBlogSettings(BlogItem blog);
    }

    public class CustomFieldRepository : Repository<CustomField>, ICustomFieldRepository
    {
        AppDbContext _db;

        public CustomFieldRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public Task<BlogItem> GetBlogSettings()
        {
            var blog = new BlogItem();
            CustomField title, desc, items, cover, logo, theme;

            title = _db.CustomFields.Where(f => f.AuthorId == 0 && f.Name == Constants.BlogTitle).FirstOrDefault();
            desc = _db.CustomFields.Where(f => f.AuthorId == 0 && f.Name == Constants.BlogDescription).FirstOrDefault();
            items = _db.CustomFields.Where(f => f.AuthorId == 0 && f.Name == Constants.BlogItemsPerPage).FirstOrDefault();
            cover = _db.CustomFields.Where(f => f.AuthorId == 0 && f.Name == Constants.BlogCover).FirstOrDefault();
            logo = _db.CustomFields.Where(f => f.AuthorId == 0 && f.Name == Constants.BlogLogo).FirstOrDefault();
            theme = _db.CustomFields.Where(f => f.AuthorId == 0 && f.Name == Constants.BlogTheme).FirstOrDefault();

            blog.Title = title == null ? "Blog Title" : title.Content;
            blog.Description = desc == null ? "Short blog description" : desc.Content;
            blog.ItemsPerPage = items == null ? 10 : int.Parse(items.Content);
            blog.Cover = cover == null ? "lib/img/cover.png" : cover.Content;
            blog.Logo = logo == null ? "lib/img/logo-white.png" : logo.Content;
            blog.Theme = theme == null ? "Standard" : theme.Content;

            return Task.FromResult(blog);
        }

        public async Task SaveBlogSettings(BlogItem blog)
        {
            var title = _db.CustomFields.Single(f => f.AuthorId == 0 && f.Name == Constants.BlogTitle);
            var desc = _db.CustomFields.Single(f => f.AuthorId == 0 && f.Name == Constants.BlogDescription);
            var items = _db.CustomFields.Single(f => f.AuthorId == 0 && f.Name == Constants.BlogItemsPerPage);
            var cover = _db.CustomFields.Single(f => f.AuthorId == 0 && f.Name == Constants.BlogCover);
            var logo = _db.CustomFields.Single(f => f.AuthorId == 0 && f.Name == Constants.BlogLogo);
            var theme = _db.CustomFields.Single(f => f.AuthorId == 0 && f.Name == Constants.BlogTheme);

            if (title == null) _db.CustomFields.Add(new CustomField { AuthorId = 0, Name = Constants.BlogTitle, Content = blog.Title });
            else title.Content = blog.Title;

            if (desc == null) _db.CustomFields.Add(new CustomField { AuthorId = 0, Name = Constants.BlogDescription, Content = blog.Description });
            else desc.Content = blog.Description;

            if (items == null) _db.CustomFields.Add(new CustomField { AuthorId = 0, Name = Constants.BlogItemsPerPage, Content = blog.ItemsPerPage.ToString() });
            else items.Content = blog.ItemsPerPage.ToString();

            if (cover == null) _db.CustomFields.Add(new CustomField { AuthorId = 0, Name = Constants.BlogCover, Content = blog.Cover });
            else cover.Content = blog.Cover;

            if (logo == null) _db.CustomFields.Add(new CustomField { AuthorId = 0, Name = Constants.BlogLogo, Content = blog.Logo });
            else logo.Content = blog.Logo;

            if (theme == null) _db.CustomFields.Add(new CustomField { AuthorId = 0, Name = Constants.BlogTheme, Content = blog.Theme });
            else theme.Content = blog.Theme;

            await _db.SaveChangesAsync();
        }
    }
}