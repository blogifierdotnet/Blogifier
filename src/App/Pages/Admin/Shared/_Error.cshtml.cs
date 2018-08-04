using Core.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Pages.Admin.Shared
{
    public class _ErrorModel : AdminPageModel
    {
        [BindProperty]
        public int Code { get; set; }

        IUnitOfWork _db;

        public _ErrorModel(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task OnGet(int code)
        {
            Author = await _db.Authors.GetItem(a => a.AppUserName == User.Identity.Name);
            Code = code;
        }
    }
}