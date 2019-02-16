using Core.Helpers;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Widgets
{
    [ViewComponent(Name = "Newsletter")]
    public class Newsletter : ViewComponent
    {
        IDataService _db;

        public Newsletter(IDataService db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke(string theme, string widget)
        {
            var keyHeader = $"{theme}-{widget}-header";
            var keyThankyou = $"{theme}-{widget}-thankyou";

            var header = _db.CustomFields.GetCustomValue(keyHeader);
            var thankyou = _db.CustomFields.GetCustomValue(keyThankyou);

            if (string.IsNullOrEmpty(header)) { header = "<p>Subscribe to our newsletter to get latest posts delivered directly to your inbox.</p>"; }
            if (string.IsNullOrEmpty(thankyou)) { thankyou = "Thank you!"; }

            var model = new NewsletterModel { Header = header, ThankYou = thankyou };

            return View("~/Views/Widgets/Newsletter/Index.cshtml", model);
        }
    }

    [Route("widgets/newsletter")]
    public class NewsletterController : Controller
    {
        IDataService _db;

        public NewsletterController(IDataService db)
        {
            _db = db;
        }

        [Route("load")]
        public async Task<NewsletterModel> Load(int page = 1)
        {
            var pager = new Pager(page);
            IEnumerable<Core.Data.Newsletter> items;

            items = await _db.Newsletters.GetList(e => e.Id > 0, pager);

            if (page < 1 || page > pager.LastPage)
                return null;

            return new NewsletterModel
            {
                Emails = items,
                Pager = pager
            };
        }

        [HttpPut]
        [Route("subscribe")]
        public IActionResult Subscribe([FromBody]string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                try
                {
                    var existing = _db.Newsletters.Single(n => n.Email == email);
                    if (existing == null)
                    {
                        _db.Newsletters.Add(new Core.Data.Newsletter { Email = email });
                        _db.Complete();
                    }
                }
                catch (System.Exception ex)
                {
                    return Ok(ex.Message);
                }
            }
            return Ok();
        }

        [HttpPut]
        [Route("unsubscribe")]
        public IActionResult Unsubscribe([FromBody]string email)
        {
            var existing = _db.Newsletters.Single(n => n.Email == email);
            if (existing != null)
            {
                _db.Newsletters.Remove(existing);
                _db.Complete();
            }
            return Ok();
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult Edit(string txtHeader, string txtThankYou, string hdnWidget, string hdnTheme)
        {
            var keyHeader = $"{hdnTheme}-{hdnWidget}-header";
            var keyThankyou = $"{hdnTheme}-{hdnWidget}-thankyou";

            _db.CustomFields.SaveCustomValue(keyHeader, txtHeader);
            _db.CustomFields.SaveCustomValue(keyThankyou, txtThankYou);

            return Redirect(Core.Constants.ThemeEditReturnUrl);
        }
    }

    public class NewsletterModel
    {
        public string Header { get; set; }
        public string ThankYou { get; set; }
        public IEnumerable<Core.Data.Newsletter> Emails { get; set; }
        public Pager Pager { get; set; }
    }
}