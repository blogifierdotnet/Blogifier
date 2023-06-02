using Blogifier.Newsletters;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Interfaces;

[Route("api/mail")]
[ApiController]
[Authorize]
public class MailController : ControllerBase
{
  private readonly EmailManager _emailManager;
  public MailController(EmailManager emailManager)
  {
    _emailManager = emailManager;
  }

  [HttpGet("settings")]
  public async Task<MailSettingDto?> GetSettingsAsync()
  {
    return await _emailManager.GetSettingsAsync();
  }

  [HttpPut("settings")]
  public async Task PutSettingsAsync([FromBody] MailSettingDto input)
  {
    await _emailManager.PutSettingsAsync(input);
  }
}
