using Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

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

        public IViewComponentResult Invoke(string theme, string widget)
        {
            var keyAuth = $"{theme}-{widget}-auth";
            var keyCat = $"{theme}-{widget}-cat";
            var keyMax = $"{theme}-{widget}-max";

            var selectedAuth = _db.CustomFields.GetCustomValue(keyAuth);
            var selectedCat = _db.CustomFields.GetCustomValue(keyCat);
            var maxRecords = _db.CustomFields.GetCustomValue(keyMax);

            if (selectedAuth == "All") { selectedAuth = ""; }
            if (selectedCat == "All") { selectedCat = ""; }
            if (string.IsNullOrEmpty(maxRecords)) { maxRecords = "10"; }

            var posts = _db.BlogPosts.All()
                .Where(p => p.Published > DateTime.MinValue)
                .OrderByDescending(p => p.Published).ToList();

            var auth = _db.Authors.All().Where(a => a.DisplayName == selectedAuth).FirstOrDefault();

            if (!string.IsNullOrEmpty(selectedAuth) && !string.IsNullOrEmpty(selectedCat))
            {
                var p1 = posts.Where(p => p.Categories != null && p.Categories.Contains(selectedCat) && p.AuthorId == auth.Id);
                if (p1 != null) posts = p1.ToList();
            }
            else if (!string.IsNullOrEmpty(selectedAuth))
            {
                var p2 = posts.Where(p => p.AuthorId == auth.Id).ToList();
                if (p2 != null) posts = p2.ToList();
            }
            else if (!string.IsNullOrEmpty(selectedCat))
            {
                var p3 = posts.Where(p => p.Categories != null && p.Categories.Contains(selectedCat)).ToList();
                if (p3 != null) posts = p3.ToList();
            }

            int maxRec;
            if (!int.TryParse(maxRecords, out maxRec))
                maxRec = 10;

            var model = posts.Take(maxRec).ToList();

            return View("~/Views/Widgets/PostList/Index.cshtml", model);
        }
    }

    [Route("widgets/api/postlist")]
    public class PostListController : Controller
    {
        IDataService _db;

        public PostListController(IDataService db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult Edit(string selAuthors, string selCats, string txtMaxRecords, string hdnWidget, string hdnTheme)
        {
            var keyAuth = $"{hdnTheme}-{hdnWidget}-auth";
            var keyCat = $"{hdnTheme}-{hdnWidget}-cat";
            var keyMax = $"{hdnTheme}-{hdnWidget}-max";

            _db.CustomFields.SaveCustomValue(keyAuth, selAuthors);
            _db.CustomFields.SaveCustomValue(keyCat, selCats);
            _db.CustomFields.SaveCustomValue(keyMax, txtMaxRecords);

            return Redirect("~/admin/settings/themes");
        }
    }
}