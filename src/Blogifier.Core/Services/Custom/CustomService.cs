using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Services.Custom
{
    public class CustomService : ICustomService
    {
        IUnitOfWork _db;

        public CustomService(IUnitOfWork db)
        {
            _db = db;
        }

        public Task<Dictionary<string, string>> GetProfileCustomFields(Profile profile)
        {
            var fields = new Dictionary<string, string>();
            if(profile != null)
            {
                var dbFields = _db.CustomFields.Find(f => f.CustomType == CustomType.Profile && f.ParentId == profile.Id);
                if (dbFields != null && dbFields.Count() > 0)
                {
                    foreach (var field in dbFields)
                    {
                        fields.Add(field.CustomKey, field.CustomValue);
                    }
                }
            }
            return Task.Run(()=> fields);
        }
    }
}
