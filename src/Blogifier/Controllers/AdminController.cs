using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

public class AdminController : Controller
{
  [HttpGet("/admin")]
  public async Task<IActionResult> Admin() => await Task.FromResult(File("~/index.html", "text/html"));
}
