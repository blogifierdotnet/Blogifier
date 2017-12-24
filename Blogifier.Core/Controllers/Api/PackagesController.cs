using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services.Packages;
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
        IPackageService _pkgs;
        ILogger _logger;

        public PackagesController(IUnitOfWork db, IPackageService pkgs, ILogger<AssetsController> logger)
        {
            _db = db;
            _pkgs = pkgs;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<PackageListItem> GetSingle(string id)
        {
            return await _pkgs.Single(id);
        }

        [HttpPut("enable/{id}")]
        public async Task Enable(string id)
        {
            var disabled = await Disabled();
            if (disabled != null && disabled.Contains(id))
            {
                disabled.Remove(id);
                await _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.DisabledPackages, string.Join(",", disabled));
            }
        }

        [HttpPut("disable/{id}")]
        public async Task Disable(string id)
        {
            var disabled = await Disabled();
            if (disabled == null)
            {
                await _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.DisabledPackages, id);
            }
            else
            {
                if (!disabled.Contains(id))
                {
                    disabled.Add(id);
                    await _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.DisabledPackages, string.Join(",", disabled));
                }
            }
        }

        async Task<List<string>> Disabled()
        {
            var field = await _db.CustomFields.GetValue(CustomType.Application, 0, Constants.DisabledPackages);
            return string.IsNullOrEmpty(field) ? null : field.Split(',').ToList();
        }

        async Task<Profile> GetProfile()
        {
            try
            {
                return await _db.Profiles.Single(p => p.IdentityName == User.Identity.Name);
            }
            catch
            {
                RedirectToAction("Login", "Account");
            }
            return null;
        }
    }
}