using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Controllers.Api
{
    [Authorize]
    [Route("blogifier/api/[controller]")]
    public class PackagesController : Controller
    {
        IUnitOfWork _db;
        ILogger _logger;
        string key = "DISABLED-PACKAGES";

        public PackagesController(IUnitOfWork db, ILogger<AssetsController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpPut("enable/{id}")]
        public async Task Enable(string id)
        {
            var disabled = Disabled();
            if (disabled != null && disabled.Contains(id))
            {
                disabled.Remove(id);
                await _db.CustomFields.SetCustomField(CustomType.Application, 0, key, string.Join(",", disabled));
            }
        }

        [HttpPut("disable/{id}")]
        public async Task Disable(string id)
        {
            var disabled = Disabled();
            if (disabled == null)
            {
                await _db.CustomFields.SetCustomField(CustomType.Application, 0, key, id);
            }
            else
            {
                if (!disabled.Contains(id))
                {
                    disabled.Add(id);
                    await _db.CustomFields.SetCustomField(CustomType.Application, 0, key, string.Join(",", disabled));
                }
            }
        }

        List<string> Disabled()
        {
            var field = _db.CustomFields.Single(f => f.CustomType == CustomType.Application && f.CustomKey == key);
            return field == null || string.IsNullOrEmpty(field.CustomValue) ? null : field.CustomValue.Split(',').ToList();
        }

        Profile GetProfile()
        {
            try
            {
                return _db.Profiles.Single(p => p.IdentityName == User.Identity.Name);
            }
            catch
            {
                RedirectToAction("Login", "Account");
            }
            return null;
        }
    }
}