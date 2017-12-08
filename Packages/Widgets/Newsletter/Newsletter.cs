using Microsoft.AspNetCore.Mvc;

namespace Blogifier.Widgets
{
    [ViewComponent(Name = "Newsletter")]
    public class Newsletter : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}