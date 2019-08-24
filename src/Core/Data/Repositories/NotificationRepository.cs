using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Data
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<IEnumerable<Notification>> GetList(Expression<Func<Notification, bool>> predicate, Pager pager);
    }

    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        AppDbContext _db;

        public NotificationRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Notification>> GetList(Expression<Func<Notification, bool>> predicate, Pager pager)
        {
            var take = pager.ItemsPerPage == 0 ? 10 : pager.ItemsPerPage;
            var skip = pager.CurrentPage * take - take;

            var messages = _db.Notifications.Where(predicate)
                .OrderByDescending(e => e.Id).ToList();

            pager.Configure(messages.Count);

            var list = messages.Skip(skip).Take(take).ToList();

            return await Task.FromResult(list);
        }
    }
}
