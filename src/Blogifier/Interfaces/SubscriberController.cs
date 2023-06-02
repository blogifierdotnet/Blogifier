using Blogifier.Newsletters;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Interfaces;

[Route("api/subscriber")]
[ApiController]
[Authorize]
public class SubscriberController : ControllerBase
{
  private readonly SubscriberProvider _subscriberProvider;

  public SubscriberController(SubscriberProvider subscriberProvider)
  {
    _subscriberProvider = subscriberProvider;
  }

  [HttpGet("items")]
  public async Task<IEnumerable<SubscriberDto>> GetItemsAsync()
  {
    return await _subscriberProvider.GetItemsAsync();
  }

  [HttpDelete("{id:int}")]
  public async Task DeleteAsync([FromRoute] int id)
  {
    await _subscriberProvider.DeleteAsync(id);
  }
}
