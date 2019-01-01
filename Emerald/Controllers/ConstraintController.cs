using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Emerald.Controllers
{
    public class ConstraintController : Controller
    {
        //[Route("/{args}")]
        public IActionResult Hello(string args)
        {
            //return View();
            return Ok($"Hello World! , Msg: {args}");
        }
    }
}