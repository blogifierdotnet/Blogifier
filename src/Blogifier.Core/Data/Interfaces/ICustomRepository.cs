using Blogifier.Core.Data.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Core.Data.Interfaces
{
    public interface ICustomRepository : IRepository<CustomField>
    {
        Task<Dictionary<string, string>> GetCustomFields(CustomType customType, int parentId);
        Task SetCustomField(CustomType customType, int parentId, string key, string value);
        string GetValue(CustomType customType, int parentId, string key);
    }
}