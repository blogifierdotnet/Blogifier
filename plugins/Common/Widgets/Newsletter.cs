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

        [HttpPut]
        [Route("subscribe")]
        public IActionResult Subscribe([FromBody]string email)
        {
            var existing = _db.Newsletters.Single(n => n.Email == email);

            if(existing == null)
            {
                _db.Newsletters.Add(new Core.Data.Newsletter { Email = email });
                _db.Complete();
            }
            return Ok("Thank you!");
        }
    }
}