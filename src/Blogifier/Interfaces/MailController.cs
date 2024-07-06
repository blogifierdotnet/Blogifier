using Blogifier.Newsletters;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Interfaces;

[Route("api/mail")]
[ApiController]
[Authorize]
public class MailController(EmailManager emailManager) : ControllerBase
{
  private readonly EmailManager _emailManager = emailManager;

  [HttpGet("settings")]
  public async Task<MailSettingDto?> GetSettingsAsync() => await _emailManager.GetSettingsAsync();

  [HttpPut("settings")]
  public async Task PutSettingsAsync([FromBody] MailSettingDto input) => await _emailManager.PutSettingsAsync(input);
}
