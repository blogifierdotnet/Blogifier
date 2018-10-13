using Core.Data;
using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetNotifications(int authorId);
        Task<int> AddNotification(AlertType aType, int authorId, string notifier, string content);
    }

    public class NotificationService : INotificationService
    {
        static DateTime _checkPoint;
        IDataService _db;
        IWebService _web;

        public NotificationService(IDataService db, IWebService web)
        {
            _db = db;
            _web = web;
        }

        public async Task<int> AddNotification(AlertType aType, int authorId, string notifier, string content)
        {
            var existing = _db.Notifications.Single(n => n.Content == content);
            if(existing == null)
            {
                _db.Notifications.Add(new Notification
                {
                    AlertType = aType,
                    AuthorId = authorId,
                    Notifier = notifier,
                    Content = content,
                    Active = true,
                    DateNotified = SystemClock.Now()
                });
                _db.Complete();
            }
            return await Task.FromResult(0);
        }

        public async Task<IEnumerable<Notification>> GetNotifications(int authorId)
        {
            if(SystemClock.Now() >= _checkPoint)
            {
                _checkPoint = SystemClock.Now().AddMinutes(30);
                var result = await _web.CheckForLatestRelease();

                if (!string.IsNullOrEmpty(result))
                {
                    await AddNotification(AlertType.Primary, 0, "Github", result);
                }
            }

            var notes = _db.Notifications
                .Find(n => n.Active && (n.AuthorId == 0 || n.AuthorId == authorId))
                .OrderByDescending(n => n.DateNotified)
                .Take(5);

            return await Task.FromResult(notes);
        }
    }
}