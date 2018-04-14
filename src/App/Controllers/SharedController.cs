using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class SharedController : Controller
    {
        [Route("/admin")]
        public IActionResult Admin()
        {
            return RedirectToAction(nameof(Index), nameof(Content));
        }

        [Route("/error/{code:int}")]
        public IActionResult Index(int code)
        {
            return View("~/Views/Shared/_Error.cshtml", code);
        }
    }
}