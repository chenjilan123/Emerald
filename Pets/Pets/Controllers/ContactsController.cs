using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pets.Model;

namespace Pets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        [HttpGet]
        public Contact Get()
        {
            return new Contact() { FirstName = "Jack", ID = "X5", LastName = "JS" };
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public ActionResult<Contact> Post(Contact contact)
        {
            return CreatedAtAction(nameof(Get), contact);
        }
    }
}