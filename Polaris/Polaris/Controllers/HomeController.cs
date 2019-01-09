using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polaris.Models;

namespace Polaris.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index(string message)
        {
            if (message == "true")
            {
                //https://localhost:44368/?message=true&hehe=adfa&cnm=5
                throw new Exception();
            }
            _logger.LogInformation("IndexIndexIndexIndexIndexIndexIndexIndexIndexIndexIndexIndexIndex");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// By default, an app doesn't provide a rich status code page for HTTP status codes, such as 404 Not Found. To provide status code pages, use Status Code Pages Middleware.
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        ///// <summary>
        ///// Error
        ///// </summary>
        ///// <returns></returns>
        //[AllowAnonymous]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel
        //    { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
