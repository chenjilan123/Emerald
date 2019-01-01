using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Emerald.Controllers
{
    public class SlugifyDemoController : Controller
    {
        public IActionResult GetAll()
        {
            return Ok("Hello, World!");
        }
    }
}