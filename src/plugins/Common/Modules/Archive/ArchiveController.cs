using Core;
using Microsoft.AspNetCore.Mvc;

namespace Common.Modules.Archive
{
    public class ArchiveController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Logo = $"{Url.Content("~/")}{AppSettings.Logo}";
            ViewBag.Cover = $"{Url.Content("~/")}{AppSettings.Cover}";
            ViewBag.Title = AppSettings.Title;
            ViewBag.Description = AppSettings.Description;

            return View("~/Views/Modules/Archive/Index.cshtml");
        }
    }
}