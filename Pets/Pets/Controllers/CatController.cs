using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pets.Data.Model;
using Pets.Data.Repositories;

namespace Pets.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CatController : ControllerBase
    {
        private CatRepository _repository;

        public CatController(CatRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cat>>> GetAllAsync()
        {
            return await _repository.GetCatsAsync();
        }

        [HttpPost]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Cat>> CreateAsync(Cat cat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _repository.AddAsync(cat);
            return CreatedAtAction(nameof(GetAllAsync), new { Id = cat.Id }, cat);
        }
    }
}