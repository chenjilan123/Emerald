using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Pets.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [FormatFilter]
    public class ProductsController : ControllerBase
    {
        //该Route将会叠加到类的Route
        [Route("[controller]/[action]/{id}.{format?}")]  //xml无效？
        [HttpGet]
        public Product GetById(int id)
        {
            //  Route                           Formatter
            /// products / GetById / 5 The      default output formatter
            /// products / GetById / 5.json     The JSON formatter(if configured)
            /// products / GetById / 5.xml      The XML formatter(if configured)
            return new Product() { Id = id };
        }
    }

    public class Product
    {
        public int Id { get; set; }
    }
}