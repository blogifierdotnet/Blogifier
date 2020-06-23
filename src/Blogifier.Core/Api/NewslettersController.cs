using Blogifier.Core.Data;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Helpers;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewslettersController: ControllerBase
    {
        IDataService _data;
        INewsletterService _newsletterService;

        public NewslettersController(IDataService data, INewsletterService newsletterService)
        {
            _data = data;
            _newsletterService = newsletterService;
        }

        [HttpGet]
        [Administrator]
        public async Task<IEnumerable<Newsletter>> Get()
        {
            var pager = new Pager(1, 10000);
            return await _data.Newsletters.GetList(e => e.Id > 0, pager);
        }

        [HttpGet("search")]
        [Administrator]
        public async Task<IEnumerable<Newsletter>> Search([FromQuery] string term)
        {
            var pager = new Pager(1, 100);
            return await _data.Newsletters.GetList(e => e.Email.Contains(term) || e.Ip.Contains(term), pager);
        }

        /// <summary>
        /// List of newsletter subscriptions (admins only)
        /// </summary>
        /// <param name="page">Page number</param>
        /// <returns>List of subscribers</returns>
        [HttpGet("subscriptions")]
        [Administrator]
        public async Task<NewsletterModel> Subscriptions(int page = 1)
        {
            var pager = new Pager(page, 20);
            IEnumerable<Newsletter> items;

            items = await _data.Newsletters.GetList(e => e.Id > 0, pager);

            if (page < 1 || page > pager.LastPage)
                return null;

            return new NewsletterModel
            {
                Emails = items,
                Pager = pager
            };
        }

        [HttpPut("subscribe")]
        [EnableCors("AllowOrigin")]
        public IActionResult Subscribe([FromBody] Newsletter letter)
        {
            if (letter != null && letter.Email.IsEmail())
            {
                try
                {
                    var existing = _data.Newsletters.Single(n => n.Email == letter.Email);
                    if (existing == null)
                    {
                        var newLetter = new Newsletter
                        {
                            Email = letter.Email,
                            Ip = string.IsNullOrEmpty(letter.Ip) ? "n/a" : letter.Ip,
                            Created = SystemClock.Now()
                        };
                        _data.Newsletters.Add(newLetter);
                        _data.Complete();
                    }
                }
                catch (Exception ex)
                {
                    return Ok(ex.Message);
                }
            }
            return Ok();
        }

        [HttpPut("unsubscribe")]
        [EnableCors("AllowOrigin")]
        public IActionResult Unsubscribe(string email)
        {
            if (!string.IsNullOrEmpty(email) && email.IsEmail())
            {
                try
                {
                    var existing = _data.Newsletters.Single(n => n.Email == email);
                    if (existing != null)
                    {
                        _data.Newsletters.Remove(existing);
                        _data.Complete();
                    }
                }
                catch (Exception ex)
                {
                    return Ok(ex.Message);
                }
            }
            return Ok();
        }

        [HttpGet("send/{postId}")]
        [Administrator]
        public async Task<int> SendNewsletters(int postId)
        {
            var pager = new Pager(1, 10000);
            var items = await _data.Newsletters.GetList(e => e.Id > 0, pager);
            var emails = items.Select(i => i.Email).ToList();
            int count = 0;

            if (emails.Count > 0)
            {
                var blogPost = _data.BlogPosts.Single(p => p.Id == postId);
                string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/";
                count = await _newsletterService.SendNewsletters(blogPost, emails, baseUrl);
            }
            return count;
        }

        [Administrator]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existing = _data.Newsletters.Single(n => n.Id == id);
                if (existing != null)
                {
                    _data.Newsletters.Remove(existing);
                    _data.Complete();
                }
                return Ok(Resources.Removed);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
