using Blogifier.Core.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blogifier.Widgets
{
    [ViewComponent(Name = "Newsletter")]
    public class Newsletter : ViewComponent
    {
        IUnitOfWork _db;

        public Newsletter(IUnitOfWork db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}