using Blogifier.Newsletters;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Interfaces;

[Route("api/subscriber")]
[ApiController]
public class SubscriberController : ControllerBase
{
  private readonly SubscriberProvider _subscriberProvider;

  public SubscriberController(SubscriberProvider subscriberProvider)
  {
    _subscriberProvider = subscriberProvider;
  }

  [HttpGet("items")]
  [Authorize]
  public async Task<IEnumerable<SubscriberDto>> GetItemsAsync()
  {
    return await _subscriberProvider.GetItemsAsync();
  }

  [HttpDelete("{id:int}")]
  [Authorize]
  public async Task DeleteAsync([FromRoute] int id)
  {
    await _subscriberProvider.DeleteAsync(id);
  }

  [HttpPost("apply")]
  public async Task<IActionResult> ApplyAsync([FromBody] SubscriberApplyDto input)
  {
    var res =  await _subscriberProvider.ApplyAsync(input);

    if(res == 1)
    {
      return Ok();
    }

    return BadRequest();
  }
}
