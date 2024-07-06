using Blogifier.Newsletters;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Interfaces;

[Route("api/newsletter")]
[ApiController]
[Authorize]
public class NewsletterController(NewsletterProvider newsletterProvider) : ControllerBase
{
  private readonly NewsletterProvider _newsletterProvider = newsletterProvider;

  [HttpGet("items")]
  public async Task<IEnumerable<NewsletterDto>> GetItemsAsync() => await _newsletterProvider.GetItemsAsync();

  [HttpDelete("{id:int}")]
  public async Task DeleteAsync([FromRoute] int id) => await _newsletterProvider.DeleteAsync(id);

  [HttpGet("send/{postId:int}")]
  public async Task SendNewsletter([FromRoute] int postId, [FromServices] EmailManager emailManager) =>
    await emailManager.SendNewsletter(postId);
}
