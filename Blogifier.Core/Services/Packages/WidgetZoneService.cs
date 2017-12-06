using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

    public class ZoneViewModel
    {
        public string Theme { get; set; }
        public string Zone { get; set; }
        public List<string> Widgets { get; set; }
    }
}
