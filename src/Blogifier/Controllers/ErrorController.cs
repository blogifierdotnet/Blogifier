using AutoMapper;
using Blogifier.Blogs;
using Blogifier.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

public class ErrorController : Controller
{
  protected readonly ILogger _logger;
  protected readonly IMapper _mapper;
  protected readonly MainManager _mainManager;

  public ErrorController(ILogger<ErrorController> logger, IMapper mapper, MainManager mainManager)
  {
    _logger = logger;
    _mapper = mapper;
    _mainManager = mainManager;
  }

  [Route("404")]
  public async Task<IActionResult> Error404()
  {
    try
    {
      var data = await _mainManager.GetMainAsync();
      var model = _mapper.Map<MainModel>(data);
      return View($"~/Views/Themes/{model.Theme}/404.cshtml", model);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "error page exception");
      return View($"~/Views/404.cshtml");
    }
  }
}
