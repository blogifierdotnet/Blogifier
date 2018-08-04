using Core.Data;
using System.Threading.Tasks;

namespace App.Pages.Admin.Settings
{
    public class ImportModel : AdminPageModel
    {
        IUnitOfWork _db;

        public ImportModel(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task OnGet()
        {
            Author = await _db.Authors.GetItem(a => a.AppUserName == User.Identity.Name);
        }
    }
}