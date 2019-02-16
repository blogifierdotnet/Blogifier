using Core.Data;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Widgets
{
    [ViewComponent(Name = "Categories")]
    public class Categories : ViewComponent
    {
        IDataService _db;

        public Categories(IDataService db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync(string theme, string widget)
        {
            var keyMax = $"{theme}-{widget}-max";
            var keyTmpl = $"{theme}-{widget}-tmpl";

            var max = _db.CustomFields.GetCustomValue(keyMax);
            var tmpl = _db.CustomFields.GetCustomValue(keyTmpl);

            if (string.IsNullOrEmpty(max)) max = "10";
            if (string.IsNullOrEmpty(tmpl)) tmpl = "<a href=\"/categories/{0}\" class=\"list-group-item list-group-item-action\">{0}</a>";

            var cats = await _db.BlogPosts.Categories();
            var model = new CategoryWidgetModel { Template = tmpl, Categories = new List<CategoryItem>() };

            var catList = cats.Cast<CategoryItem>().ToList();

            foreach (var cat in catList.Take(int.Parse(max)))
            {
                model.Categories.Add(cat);
            }
            return View("~/Views/Widgets/Categories/Index.cshtml", model);
        }
    }

    [Route("widgets/api/categories")]
    public class CategoriesController : Controller
    {
        IDataService _db;

        public CategoriesController(IDataService db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult Edit(string selAuthors, string selCats, string txtMaxRecords, string txtTemplate, string hdnWidget, string hdnTheme)
        {
            var keyMax = $"{hdnTheme}-{hdnWidget}-max";
            var keyTmpl = $"{hdnTheme}-{hdnWidget}-tmpl";

            _db.CustomFields.SaveCustomValue(keyMax, txtMaxRecords);
            _db.CustomFields.SaveCustomValue(keyTmpl, txtTemplate);

            return Redirect(Core.Constants.ThemeEditReturnUrl);
        }
    }

    public class CategoryWidgetModel
    {
        public string Template { get; set; }
        public List<CategoryItem> Categories { get; set; }
    }
}
