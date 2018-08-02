using Core;
using Core.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace App.Pages.Admin.Posts
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public PostItem PostItem { get; set; }

        IUnitOfWork _db;

        public EditModel(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task OnGetAsync(int id)
        {
            PostItem = new PostItem { Author = await GetAuthor(), Cover = AppSettings.Cover };

            if (id > 0)
                PostItem = await _db.BlogPosts.GetItem(p => p.Id == id);
        }

        async Task<Author> GetAuthor()
        {
            return await _db.Authors.GetItem(a => a.AppUserName == User.Identity.Name);
        }
    }
}