using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Services.Social
{
    public class SocialService : ISocialService
    {
        IUnitOfWork _db;

        public SocialService(IUnitOfWork db)
        {
            _db = db;
        }

        public Task<Dictionary<string, string>> GetSocialButtons(Profile profile)
        {
            var buttons = ApplicationSettings.SocialButtons;

            if(profile != null)
            {
                // override with profile customizations
                var dbFields = _db.CustomFields.Find(f => f.CustomType == CustomType.Profile && f.ParentId == profile.Id);
                if (dbFields != null && dbFields.Count() > 0)
                {
                    foreach (var field in dbFields)
                    {
                        if (buttons.ContainsKey(field.CustomKey))
                        {
                            buttons[field.CustomKey] = field.CustomValue;
                        }
                    }
                }
            }
            return Task.Run(()=> buttons);
        }
    }
}
