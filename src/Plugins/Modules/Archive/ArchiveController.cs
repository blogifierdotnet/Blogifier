using Microsoft.AspNetCore.Mvc;

namespace Archive.Controllers
{
    public class ArchiveController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/Modules/Archive/Index.cshtml");
        }
    }
}