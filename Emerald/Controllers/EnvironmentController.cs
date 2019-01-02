using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emerald.Models;
using Microsoft.AspNetCore.Mvc;

namespace Emerald.Controllers
{
    public class EnvironmentController : Controller
    {
        
        public IActionResult About()
        {
            //ViewData["Title"] = "Demo";
            var model = new AboutModel() { Message = "This an Demo About Environment!" };
            return View(model);

            //return "Hello";
        }
    }
}