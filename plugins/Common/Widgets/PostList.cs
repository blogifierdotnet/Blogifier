using Microsoft.AspNetCore.Mvc;

namespace Common.Widgets
{
    [ViewComponent(Name = "PostList")]
    public class PostList : ViewComponent
    {
        public PostList()
        {
        }

        public IViewComponentResult Invoke()
        {
            return View("~/Views/Widgets/PostList/Index.cshtml");
        }
    }
}