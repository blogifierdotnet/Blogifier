namespace Core.Data
{
    public interface IHtmlWidgetRepository : IRepository<HtmlWidget>
    {
    }

    public class HtmlWidgetRepository : Repository<HtmlWidget>, IHtmlWidgetRepository
    {
        AppDbContext _db;

        public HtmlWidgetRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
