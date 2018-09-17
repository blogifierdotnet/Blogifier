using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Common.Widgets
{
    [ViewComponent(Name = "HtmlBlock")]
    public class HtmlBlock : ViewComponent
    {
        IDataService _db;

        public HtmlBlock(IDataService db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke(string id, string theme, string author)
        {
            string model = @"<ul class=""blog-social nav ml-auto my-auto"">
    <li class=""blog-social-item""><a href=""#"" target=""_blank"" class=""blog-social-link""><i class=""blog-social-icon fa fa-twitter""></i></a></li>
    <li class=""blog-social-item""><a href=""#"" target=""_blank"" class=""blog-social-link""><i class=""blog-social-icon fa fa-google-plus""></i></a></li>
    <li class=""blog-social-item""><a href=""#"" target=""_blank"" class=""blog-social-link""><i class=""blog-social-icon fa fa-facebook-official""></i></a></li>
</ul>";

            var existing = _db.HtmlWidgets.Single(w => w.Name == id && w.Theme == theme && w.Author == author);

            if (existing == null)
            {
                _db.HtmlWidgets.Add(new Core.Data.HtmlWidget {
                    Name = id,
                    Theme = theme,
                    Author = author,
                    Content = model
                });
                _db.Complete();
            }
            else
            {
                model = existing.Content;
            } 

            return View("~/Views/Widgets/HtmlBlock/Index.cshtml", model);
        }
    }
}