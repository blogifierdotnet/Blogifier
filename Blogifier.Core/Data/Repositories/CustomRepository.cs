using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Data.Repositories
{
    public class CustomRepository : Repository<CustomField>, ICustomRepository
    {
        BlogifierDbContext _db;

        public CustomRepository(BlogifierDbContext db) : base(db)
        {
            _db = db;
        }

        public Task<Dictionary<string, string>> GetCustomFields(CustomType customType, int parentId)
        {
            var fields = new Dictionary<string, string>();
            IQueryable<CustomField> dbFields;

            if(parentId == 0)
            {
                dbFields = _db.CustomFields.Where(f => f.CustomType == customType).OrderBy(f => f.Title);
            }
            else
            {
                dbFields = _db.CustomFields.Where(f => f.CustomType == customType && f.ParentId == parentId).OrderBy(f => f.Title);
            }

            return Task.Run(() => Load(dbFields));
        }

        public Task<Dictionary<string, string>> GetBlogFields()
        {
            var dbFields = _db.CustomFields.Where(f => f.CustomType == CustomType.Application && f.ParentId == 0).OrderBy(f => f.Title);
            return Task.Run(() => Load(dbFields));
        }

        public Task<Dictionary<string, string>> GetUserFields(int profileId)
        {
            var dbFields = _db.CustomFields.Where(f => f.CustomType == CustomType.Profile && f.ParentId == profileId).OrderBy(f => f.Title);
            return Task.Run(() => Load(dbFields));
        }

        public string GetValue(CustomType customType, int parentId, string key)
        {
            var field = _db.CustomFields.Where(f => f.CustomType == customType && f.ParentId == parentId && f.CustomKey == key).FirstOrDefault();
            return field == null || field.CustomValue == null ? string.Empty : field.CustomValue;
        }

        public async Task<int> SetCustomField(CustomType customType, int parentId, string key, string value)
        {
            var dbField = _db.CustomFields
                .Where(f => f.CustomType == customType && f.ParentId == parentId && f.CustomKey == key)
                .FirstOrDefault();

            if (dbField != null)
            {
                dbField.CustomValue = value;
                dbField.LastUpdated = SystemClock.Now();
            }
            else
            {
                _db.CustomFields.Add(new CustomField
                {
                    CustomKey = key,
                    CustomValue = value,
                    Title = key,
                    CustomType = customType,
                    ParentId = parentId,
                    LastUpdated = SystemClock.Now()
                });
            }
            return await _db.SaveChangesAsync();
        }

        Dictionary<string, string> Load(IQueryable<CustomField> dbFields)
        {
            var fields = new Dictionary<string, string>();
            if (dbFields != null && dbFields.Count() > 0)
            {
                foreach (var field in dbFields)
                {
                    fields.Add(field.CustomKey, field.CustomValue);
                }
            }
            return fields;
        }
    }
}