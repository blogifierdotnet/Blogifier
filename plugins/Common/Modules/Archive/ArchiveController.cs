using Core.Data;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Common.Modules.Archive
{
    public class ArchiveController : Controller
    {
        IDataService _db;

        public ArchiveController(IDataService db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var model = new ListModel();

            model.Blog = await _db.CustomFields.GetBlogSettings();
            model.Blog.Cover = $"{Url.Content("~/")}{model.Blog.Cover}";

            return View("~/Views/Modules/Archive/Index.cshtml", model);
        }
    }
}