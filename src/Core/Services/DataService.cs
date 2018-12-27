using Core.Data;
using System;

namespace Core.Services
{
    public interface IDataService : IDisposable
    {
        IPostRepository BlogPosts { get; }
        IAuthorRepository Authors { get; }
        INotificationRepository Notifications { get; }
        IHtmlWidgetRepository HtmlWidgets { get; }
        ICustomFieldRepository CustomFields { get; }
        INewsletterRepository Newsletters { get; }

        int Complete();
    }

    public class DataService : IDataService
    {
        private readonly AppDbContext _db;

        public DataService(AppDbContext db)
        {
            _db = db;

            BlogPosts = new PostRepository(_db);
            Authors = new AuthorRepository(_db);
            Notifications = new NotificationRepository(_db);
            HtmlWidgets = new HtmlWidgetRepository(_db);
            CustomFields = new CustomFieldRepository(_db);
            Newsletters = new NewsletterRepository(_db);
        }

        public IPostRepository BlogPosts { get; private set; }
        public IAuthorRepository Authors { get; private set; }
        public INotificationRepository Notifications { get; private set; }
        public IHtmlWidgetRepository HtmlWidgets { get; private set; }
        public ICustomFieldRepository CustomFields { get; private set; }
        public INewsletterRepository Newsletters { get; private set; }

        public int Complete()
        {
            return _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}