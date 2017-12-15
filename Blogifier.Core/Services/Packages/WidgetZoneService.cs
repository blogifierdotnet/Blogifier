using Blogifier.Core.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blogifier.Core.Services.Packages
{
    [ViewComponent(Name = "WidgetZone")]
    public class WidgetZone : ViewComponent
    {
        public IViewComponentResult Invoke(ZoneViewModel vm)
        {
            return View(vm);
        }
    }
}