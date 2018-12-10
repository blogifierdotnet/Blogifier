using System.Linq;
using System.Threading.Tasks;

namespace Core.Data
{
    public interface ICustomFieldRepository : IRepository<CustomField>
    {
        Task<BlogItem> GetBlogSettings();
        Task SaveBlogSettings(BlogItem blog);

        string GetCustomValue(string name);
        Task SaveCustomValue(string name, string value);
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
            CustomField title, desc, items, cover, logo, theme, culture;

            title = _db.CustomFields.Where(f => f.AuthorId == 0 && f.Name == Constants.BlogTitle).FirstOrDefault();
            desc = _db.CustomFields.Where(f => f.AuthorId == 0 && f.Name == Constants.BlogDescription).FirstOrDefault();
            items = _db.CustomFields.Where(f => f.AuthorId == 0 && f.Name == Constants.BlogItemsPerPage).FirstOrDefault();
            cover = _db.CustomFields.Where(f => f.AuthorId == 0 && f.Name == Constants.BlogCover).FirstOrDefault();
            logo = _db.CustomFields.Where(f => f.AuthorId == 0 && f.Name == Constants.BlogLogo).FirstOrDefault();
            theme = _db.CustomFields.Where(f => f.AuthorId == 0 && f.Name == Constants.BlogTheme).FirstOrDefault();
            culture = _db.CustomFields.Where(f => f.AuthorId == 0 && f.Name == Constants.Culture).FirstOrDefault();

            blog.Title = title == null ? "Blog Title" : title.Content;
            blog.Description = desc == null ? "Short blog description" : desc.Content;
            blog.ItemsPerPage = items == null ? 10 : int.Parse(items.Content);
            blog.Cover = cover == null ? "lib/img/cover.png" : cover.Content;
            blog.Logo = logo == null ? "lib/img/logo-white.png" : logo.Content;
            blog.Theme = theme == null ? "Standard" : theme.Content;
            blog.Culture = culture == null ? "en-US" : culture.Content;

            return Task.FromResult(blog);
        }

        public async Task SaveBlogSettings(BlogItem blog)
        {
            var title = _db.CustomFields.Where(f => f.AuthorId == 0 && f.Name == Constants.BlogTitle).FirstOrDefault();
            var desc = _db.CustomFields.Where(f => f.AuthorId == 0 && f.Name == Constants.BlogDescription).FirstOrDefault();
            var items = _db.CustomFields.Where(f => f.AuthorId == 0 && f.Name == Constants.BlogItemsPerPage).FirstOrDefault();
            var cover = _db.CustomFields.Where(f => f.AuthorId == 0 && f.Name == Constants.BlogCover).FirstOrDefault();
            var logo = _db.CustomFields.Where(f => f.AuthorId == 0 && f.Name == Constants.BlogLogo).FirstOrDefault();
            var culture = _db.CustomFields.Where(f => f.AuthorId == 0 && f.Name == Constants.Culture).FirstOrDefault();

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

            if (culture == null) _db.CustomFields.Add(new CustomField { AuthorId = 0, Name = Constants.Culture, Content = blog.Culture });
            else culture.Content = blog.Culture;

            await _db.SaveChangesAsync();
        }

        public string GetCustomValue(string name)
        {
            var field = _db.CustomFields.Where(f => f.Name == name).FirstOrDefault();
            return field == null ? "" : field.Content;
        }

        public async Task SaveCustomValue(string name, string value)
        {
            var field = _db.CustomFields.Where(f => f.Name == name).FirstOrDefault();
            if(field == null)
            {
                _db.CustomFields.Add(new CustomField { Name = name, Content = value, AuthorId = 0 });
            }
            else
            {
                field.Content = value;
            }
            await _db.SaveChangesAsync();
        }
    }
}