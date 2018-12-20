namespace Core.Data
{
    public interface INewsletterRepository : IRepository<Newsletter>
    {
    }

    public class NewsletterRepository : Repository<Newsletter>, INewsletterRepository
    {
        AppDbContext _db;

        public NewsletterRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
