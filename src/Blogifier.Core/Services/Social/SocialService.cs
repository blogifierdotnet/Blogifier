using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Core.Services.Social
{
    public class SocialService : ISocialService
    {
        public Task<Dictionary<string, string>> GetSocialButtons(Profile profile)
        {
            var buttons = ApplicationSettings.SocialButtons;

            if(profile != null)
            {
                // override with profile customizations
            }
            return Task.Run(()=> buttons);
        }
    }
}
