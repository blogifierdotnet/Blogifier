using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Core.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        IDataService _data;

        public SecurityController(IDataService data)
        {
            _data = data;
        }

        /// <summary>
        /// Get user authentication info
        /// </summary>
        /// <returns>User authentication object</returns>
        [HttpGet("user")]
        public IActionResult GetUser()
        {
            var auth = new AuthModel { userName = "Guest" };

            if (!User.Identity.IsAuthenticated)
                return Ok(auth);
            
            var author = _data.Authors.Single(a => a.AppUserName == User.Identity.Name);

            if (author == null)
                return Ok(auth);

            auth.userName = User.Identity.Name;
            auth.isAdmin = author.IsAdmin;
            auth.isAuthenticated = true;

            return Ok(auth);
        }
    }

    public class AuthModel
    {
        public string userName { get; set; }
	    public bool isAuthenticated { get; set; }
	    public bool isAdmin { get; set; }
    }
}