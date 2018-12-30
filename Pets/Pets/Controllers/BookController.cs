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
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookService;

        public BookController(IBookRepository bookService)
        {
            this._bookService = bookService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            return _bookService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<Book> Get(string id)
        {
            var book = _bookService.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public ActionResult<Book> Create(Book book)
        {
            _bookService.Create(book);
            return CreatedAtRoute("GetBook", book);//new { id = book.Id.ToString() }, book);
        }

        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Update(string id, Book bookIn)
        {
            var book = _bookService.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            _bookService.Update(id, bookIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Delete(string id)
        {
            var book = _bookService.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            _bookService.Remove(book);
            return NoContent();
        }
    }
}