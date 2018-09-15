using Core.Data;
using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetNotifications(Expression<Func<Notification, bool>> predicate);
    }

    public class NotificationService : INotificationService
    {
        static DateTime _lastChecked;
        IDataService _db;

        public NotificationService(IDataService db)
        {
            _db = db;
            _lastChecked = SystemClock.Now();
        }

        public async Task<IEnumerable<Notification>> GetNotifications(Expression<Func<Notification, bool>> predicate)
        {
            if(DateTime.UtcNow >= _lastChecked.AddMinutes(5))
            {
                _lastChecked = SystemClock.Now();

                // get latest version from github repo...
            }

            return await Task.FromResult(_db.Notifications.Find(predicate));
        }
    }
}