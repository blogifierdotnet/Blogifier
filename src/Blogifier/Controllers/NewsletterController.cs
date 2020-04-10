using Blogifier.Core;
using Blogifier.Core.Data;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Blogifier.Controllers
{
    public class NewsletterController : Controller
    {
        protected IDataService DataService;

        public NewsletterController(IDataService dataService)
        {
            DataService = dataService;
        }

        [HttpPost]
        public IActionResult Subscribe([FromBody]Newsletter letter)
        {
            if (letter != null && letter.Email.IsEmail())
            {
                try
                {
                    var existing = DataService.Newsletters.Single(n => n.Email == letter.Email);
                    if (existing == null)
                    {
                        DataService.Newsletters.Add(new Newsletter { Email = letter.Email });
                        DataService.Complete();
                    }
                }
                catch (Exception ex)
                {
                    return Ok(ex.Message);
                }
            }
            return Ok();
        }

        [HttpPut]
        public IActionResult Unsubscribe(string email)
        {
            if (!string.IsNullOrEmpty(email) && email.IsEmail())
            {
                try
                {
                    var existing = DataService.Newsletters.Single(n => n.Email == email);
                    if (existing != null)
                    {
                        DataService.Newsletters.Remove(existing);
                        DataService.Complete();
                    }
                }
                catch (Exception ex)
                {
                    return Ok(ex.Message);
                }
            }
            return Ok();
        }
    }
}