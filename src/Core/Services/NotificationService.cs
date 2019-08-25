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
        Task PullSystemNotifications();
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
            var existing = _db.Notifications.Single(n => 
                n.AlertType == aType && 
                n.Notifier == notifier && 
                n.Content == content
            );

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

        public async Task PullSystemNotifications()
        {
            if (SystemClock.Now() >= _checkPoint)
            {
                _checkPoint = SystemClock.Now().AddMinutes(30);
                var messages = await _web.GetNotifications();

                if(messages != null && messages.Count > 0)
                {
                    foreach (var msg in messages)
                    {
                        await AddNotification(AlertType.System, 0, "Blogifier", msg);
                    }
                }
            }
        }
    }
}