using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blogifier.Core.Controllers.Api.Public
{
    [Route("blogifier/api/public/[controller]")]
    public class CategoriesController : Controller
    {
        IUnitOfWork _db;
        private readonly ILogger _logger;

        public CategoriesController(IUnitOfWork db, ILogger<BlogController> logger)
        {
            _db = db;
            _logger = logger;
        }

        public IEnumerable<Category> Get()
        {
            return _db.Categories.Find(c => c.LastUpdated >= DateTime.MinValue);            
        }
    }
}
