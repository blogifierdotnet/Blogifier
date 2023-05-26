using AutoMapper;
using Blogifier.Blogs;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

public class ErrorController : Controller
{
  protected readonly ILogger _logger;
  protected readonly IMapper _mapper;
  protected readonly BlogManager _blogManager;

  public ErrorController(
    ILogger<ErrorController> logger,
    IMapper mapper,
    BlogManager blogManager)
  {
    _logger = logger;
    _mapper = mapper;
    _blogManager = blogManager;
  }

  [Route("404")]
  public async Task<IActionResult> Error404()
  {
    try
    {
      var data = await _blogManager.GetAsync();
      var dataDto = _mapper.Map<MainDto>(data);
      var model = new MainModel(dataDto);
      return View($"~/Views/Themes/{data.Theme}/404.cshtml", model);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "error page exception");
      return View($"~/Views/404.cshtml");
    }
  }
}