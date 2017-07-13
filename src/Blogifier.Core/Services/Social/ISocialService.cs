using Blogifier.Core.Data.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Core.Services.Social
{
    public interface ISocialService
    {
        Task<Dictionary<string, string>> GetSocialButtons(Profile profile);
    }
}