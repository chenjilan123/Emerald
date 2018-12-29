using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pets.Model;
using Pets.Repository;

namespace Pets.Controllers
{
    //[Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CatController : ControllerBase
    {
        private ICatRepository _repository;

        public CatController(ICatRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cat>>> GetAllAsync()
        {
            return await _repository.GetCatsAsync();
        }

        [HttpGet("id")]
        [ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Cat>> GetCatAsync(int id)
        {
            var cat = await _repository.GetCatAsync(id);
            if (cat == null)
            {
                return NotFound();//这样好吗？
            }
            return cat;
        }

        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Cat>> CreateAsync(Cat cat)
        {
            //应用ApiController后，无需该语句。
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}
            await _repository.AddAsync(cat);
            return CreatedAtAction(nameof(GetAllAsync), new { Id = cat.Id }, cat);
        }
    }
}