using Microsoft.AspNetCore.Mvc;

namespace PostList
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
