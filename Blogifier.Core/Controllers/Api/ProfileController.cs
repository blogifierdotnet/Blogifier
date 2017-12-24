using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Core.Controllers.Api
{
    [Authorize]
    [Route("blogifier/api/[controller]")]
    public class ProfileController : Controller
    {
        IUnitOfWork _db;

        public ProfileController(IUnitOfWork db)
        {
            _db = db;
        }

        // PUT: api/profile/setcustomfield
        [HttpPut]
        [Route("setcustomfield")]
        public async Task SetCustomField([FromBody]CustomFieldItem item)
        {
            var profile = await GetProfile();
            await _db.CustomFields.SetCustomField(CustomType.Profile, profile.Id, item.CustomKey, item.CustomValue);
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