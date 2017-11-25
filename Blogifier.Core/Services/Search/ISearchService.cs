using Blogifier.Core.Common;
using Blogifier.Core.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Core.Services.Search
{
    public interface ISearchService
    {
        Task<List<PostListItem>> Find(Pager pager, string term, string blogSlug = "");
    }
}
