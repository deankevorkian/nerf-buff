using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NerfBuff.Models;

namespace NerfBuff.Controllers
{
    public class HomeController : Controller
    {
        private masterContext context = new masterContext();

        public IActionResult Index()
        {

            ViewData["Posts"] = (IEnumerable<Posts>)TempData["Posts"] ?? context.Posts.ToList();

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

		public IActionResult Events()
		{
			ViewData["Message"] = "fortnite events near your area";

			return View();
		}

		public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { ErrorMessage = "hi" ?? HttpContext.TraceIdentifier });
        }
    }
}
