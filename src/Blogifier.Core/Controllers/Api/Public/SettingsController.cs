using Blogifier.Core.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blogifier.Core.Controllers.Api.Public
{
    [Route("blogifier/api/public/[controller]/[action]")]
    public class SettingsController : Controller
    {
        IUnitOfWork _db;
        private readonly ILogger _logger;

        public SettingsController(IUnitOfWork db, ILogger<BlogController> logger)
        {
            _db = db;
            _logger = logger;
        }

        /// <summary>
        /// Get the disqus script
        /// </summary>
        public string Post()
        {
            var PostField = _db.CustomFields.Single(f => f.CustomKey == "Post");
            if(PostField != null)
            {
                return PostField.CustomValue;
            }
            return string.Empty;
        }
    }
}
