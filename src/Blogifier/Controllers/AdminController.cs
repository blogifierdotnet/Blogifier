using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

public class AdminController : Controller
{
  [HttpGet("/admin")]
  [Authorize]
  public async Task<IActionResult> Admin()
  {
    return await Task.FromResult(File("~/index.html", "text/html"));
  }
}
