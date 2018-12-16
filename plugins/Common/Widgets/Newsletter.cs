using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Common.Widgets
{
    [ViewComponent(Name = "Newsletter")]
    public class Newsletter : ViewComponent
    {
        IDataService _db;

        public Newsletter(IDataService db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke(string theme, string widget)
        {
            return View("~/Views/Widgets/Newsletter/Index.cshtml");
        }
    }

    [Route("widgets/api/newsletter")]
    public class NewsletterController : Controller
    {
        IDataService _db;

        public NewsletterController(IDataService db)
        {
            _db = db;
        }

    }
}