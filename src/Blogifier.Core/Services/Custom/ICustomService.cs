using Blogifier.Core.Data.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Core.Services.Custom
{
    public interface ICustomService
    {
        Task<Dictionary<string, string>> GetProfileCustomFields(Profile profile);
    }
}