namespace Blogifier.Core.Data
{
    public interface IStatsRepository : IRepository<StatsTotal>
    {
    }

    public class StatsRepository : Repository<StatsTotal>, IStatsRepository
    {
        AppDbContext _db;

        public StatsRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
    }
}