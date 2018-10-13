namespace Core.Data
{
    public interface ICustomFieldRepository : IRepository<CustomField>
    {
    }

    public class CustomFieldRepository : Repository<CustomField>, ICustomFieldRepository
    {
        AppDbContext _db;

        public CustomFieldRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
    }
}