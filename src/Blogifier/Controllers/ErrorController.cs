using AutoMapper;
using Blogifier.Blogs;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

public class ErrorController(
  ILogger<ErrorController> logger,
  IMapper mapper,
  MainMamager mainMamager) : Controller
{
  protected readonly ILogger _logger = logger;
  protected readonly IMapper _mapper = mapper;
  protected readonly MainMamager _mainMamager = mainMamager;

  [Route("404")]
  public async Task<IActionResult> Error404()
  {
    try
    {
      var data = await _mainMamager.GetAsync();
      var model = new MainModel(data);
      return View($"~/Views/Themes/{data.Theme}/404.cshtml", model);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "error page exception");
      return View($"~/Views/404.cshtml");
    }
  }
}
