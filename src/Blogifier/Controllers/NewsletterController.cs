using Blogifier.Core;
using Blogifier.Core.Data;
using Blogifier.Core.Helpers;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using System;

namespace Blogifier.Controllers
{
    public class NewsletterController : Controller
    {
        protected IDataService DataService;
        protected ILogger Logger;
        protected IHttpContextAccessor Accessor;

        public NewsletterController(IDataService dataService, ILogger<NewsletterController> logger, IHttpContextAccessor accessor)
        {
            DataService = dataService;
            Logger = logger;
            Accessor = accessor;
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
                            Ip = Accessor.HttpContext.Connection.RemoteIpAddress.ToString(),
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