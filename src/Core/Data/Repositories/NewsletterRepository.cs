using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Data
{
    public interface INewsletterRepository : IRepository<Newsletter>
    {
        Task<IEnumerable<Newsletter>> GetList(Expression<Func<Newsletter, bool>> predicate, Pager pager);
    }

    public class NewsletterRepository : Repository<Newsletter>, INewsletterRepository
    {
        AppDbContext _db;

        public NewsletterRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Newsletter>> GetList(Expression<Func<Newsletter, bool>> predicate, Pager pager)
        {
            var take = pager.ItemsPerPage == 0 ? 10 : pager.ItemsPerPage;
            var skip = pager.CurrentPage * take - take;

            var emails = _db.Newsletters.Where(predicate)
                .OrderByDescending(e => e.Id).ToList();

            pager.Configure(emails.Count);

            var list = emails.Skip(skip).Take(take).ToList();

            return await Task.FromResult(list);
        }
    }
}
