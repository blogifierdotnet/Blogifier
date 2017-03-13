using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
	[Route("blog")]
	public class FooController : Controller
    {
		[Route("foo")]
		public IActionResult Index()
        {
            return View();
        }
    }
}