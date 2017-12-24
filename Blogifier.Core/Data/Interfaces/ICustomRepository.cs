using Blogifier.Core.Data.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Core.Data.Interfaces
{
    public interface ICustomRepository : IRepository<CustomField>
    {
        Task<Dictionary<string, string>> GetCustomFields(CustomType customType, int parentId);
        Task<Dictionary<string, string>> GetBlogFields();
        Task<Dictionary<string, string>> GetUserFields(int profileId);

        Task<int> SetCustomField(CustomType customType, int parentId, string key, string value);

        Task<string> GetValue(CustomType customType, int parentId, string key);
    }
}