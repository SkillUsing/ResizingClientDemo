using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResizingClient;
using ResizingClientDemo.Models;

namespace ResizingClientDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(IFormFile image)
        {
            var stream = image.OpenReadStream();
            var result = await ResizingUtil.Upload(stream, $"{DateTime.Now.ToString("yyyyMMddHHmmss")}.jpg", "images");
            var path = ResizingUtil.FormatUrl(result.FormatUrl, 800, 600);
            return Content($"{path}");
        }
    }
}
