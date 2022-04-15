using System.Threading.Tasks;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Blogifier.Controllers;

[ApiController]
[Route("api/captchakey")]
public class CaptchaController : ControllerBase
{
    public CaptchaController(IConfiguration configuration)
    {
        SiteKey = configuration.GetSection("ReCaptcha").GetValue<string>("ReCaptchaSiteKey");
    }

    private string SiteKey { get; }

    [HttpGet]
    public Task<ActionResult> GetCaptchaKeyAsJson([FromQuery(Name = "valueonly")] bool keyOnly)
    {
        if (keyOnly)
            return Task.FromResult<ActionResult>(Ok(SiteKey));

        return Task.FromResult<ActionResult>(
            Ok(new CaptchaKeyModel
            {
                ReCaptchaSiteKey = SiteKey
            }));
    }
}
