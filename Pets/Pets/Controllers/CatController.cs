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
    }
}