using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CookieManagerDemo.Models;
using CookieManagerDemo.Services;

namespace CookieManagerDemo.Controllers
{
    public class HomeController : Controller
    {
		private const string c_CONTRIVEDCOOKIENAME = "contrived";
		private const string c_NAMECOOKIENAME = "basicname";

		private readonly ICookieService _cookieService;

		public HomeController(ICookieService cookieService)
		{
			_cookieService = cookieService;
		}

        public IActionResult Index()
        {
			var name = _cookieService.Get<string>(c_NAMECOOKIENAME);
			var contrived = _cookieService.GetOrSet<ContrivedValues>("contrived", () => new ContrivedValues { Name = "Guest" });

			var viewModel = new HomeViewModel
			{
				Name = name,
				Contrived = contrived
			};

            return View(viewModel);
        }

		[HttpPost]
		public IActionResult PostBasic(NameRequest request)
		{
			_cookieService.Set(c_NAMECOOKIENAME, request.Name);

			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public IActionResult PostContrived(ContrivedValues request)
		{
			_cookieService.Set(c_CONTRIVEDCOOKIENAME, request);

			return RedirectToAction(nameof(Index));
		}

		public IActionResult DeleteContrived()
		{
			_cookieService.Delete(c_CONTRIVEDCOOKIENAME);

			return RedirectToAction(nameof(Index));
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
