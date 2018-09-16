namespace Core.Data
{
    public interface INotificationRepository : IRepository<Notification>
    {
    }

    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        AppDbContext _db;

        public NotificationRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
