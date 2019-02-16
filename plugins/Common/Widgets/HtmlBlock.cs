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

        public IViewComponentResult Invoke(string theme, string widget)
        {
            string model = "";
            var existing = _db.HtmlWidgets.Single(w => w.Name == widget && w.Theme == theme && w.Author == "0");

            if (existing == null)
            {
                _db.HtmlWidgets.Add(new Core.Data.HtmlWidget {
                    Name = widget,
                    Theme = theme,
                    Author = "0",
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

    [Route("widgets/api/htmlblock")]
    public class HtmlBlockController : Controller
    {
        IDataService _db;

        public HtmlBlockController(IDataService db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult Edit(string txtWidget, string txtTheme, string txtHtml)
        {
            var existing = _db.HtmlWidgets.Single(w => w.Name == txtWidget && w.Theme == txtTheme && w.Author == "0");

            if (existing == null)
            {
                _db.HtmlWidgets.Add(new Core.Data.HtmlWidget
                {
                    Name = txtWidget,
                    Theme = txtTheme,
                    Author = "0",
                    Content = txtHtml
                });
            }
            else
            {
                existing.Content = txtHtml;
            }
            _db.Complete();
            return Redirect(Core.Constants.ThemeEditReturnUrl);
        }
    }
}