using Blogifier.Core;
using Blogifier.Core.Data;
using Blogifier.Core.Helpers;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Blogifier.Controllers
{
    public class NewsletterController : Controller
    {
        protected IDataService DataService;
        protected ILogger Logger;

        public NewsletterController(IDataService dataService, ILogger<NewsletterController> logger)
        {
            DataService = dataService;
            Logger = logger;
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
                        var newLetter = new Newsletter
                        {
                            Email = letter.Email,
                            Ip = string.IsNullOrEmpty(letter.Ip) ? "n/a" : letter.Ip,
                            Created = SystemClock.Now()
                        };
                        DataService.Newsletters.Add(newLetter);
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