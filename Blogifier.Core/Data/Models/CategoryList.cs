using Blogifier.Core.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Blogifier.Core.Data.Models
{
    public class CategoryList
    {
        public static IEnumerable<CategoryItem> Items(IUnitOfWork db, int profileId, int postId = 0)
        {
            var categories = db.Categories.Find(c => c.ProfileId == profileId, null).ToList();
            var items = new List<CategoryItem>();
            if (categories.Any())
            {
                foreach (var item in categories)
                {
                    var cnt = 0;
                    var selected = false;
                    if (item.PostCategories != null && item.PostCategories.Count > 0)
                    {
                        foreach (var pc in item.PostCategories)
                        {
                            cnt += db.BlogPosts.Single(p => p.Id == pc.BlogPostId).PostViews;
                            if (pc.BlogPostId == postId)
                            {
                                selected = true;
                            }
                        }
                    }
                    items.Add(new CategoryItem
                    {
                        Id = item.Id.ToString(),
                        Title = item.Title,
                        Slug = item.Slug,
                        PostCount = item.PostCategories == null ? 0 : item.PostCategories.Count,
                        ViewCount = cnt,
                        Selected = selected
                    });
                }
            }
            return items;
        }
    }
}
