using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Common.Widgets
{
    [ViewComponent(Name = "PostList")]
    public class PostList : ViewComponent
    {
        IDataService _db;

        public PostList(IDataService db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke()
        {
            var model = _db.BlogPosts.All();

            return View("~/Views/Widgets/PostList/Index.cshtml", model);
        }
    }
}