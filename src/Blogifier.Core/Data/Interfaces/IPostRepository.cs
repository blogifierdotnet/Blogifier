using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blogifier.Core.Data.Interfaces
{
    public interface IPostRepository : IRepository<BlogPost>
    {
        IEnumerable<PostListItem> Find(Expression<Func<BlogPost, bool>> predicate, Pager pager);
        IEnumerable<BlogPost> AllIncluded(Expression<Func<BlogPost, bool>> predicate);
        Task<List<PostListItem>> ByCategory(string slug, Pager pager, string blog = "");
        Task<BlogPost> SingleIncluded(Expression<Func<BlogPost, bool>> predicate);
        Task UpdatePostCategories(int postId, IEnumerable<string> catIds);
    }
}
