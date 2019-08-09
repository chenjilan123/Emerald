﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pets.Model;
using Pets.Services;

namespace Pets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            this._bookService = bookService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            Thread.Sleep(2000);
            return _bookService.Get();
        }


        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Book> Get(string id)
        {
            var book = _bookService.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }


        [HttpPost(Name = "PostBook")]
        public ActionResult<IEnumerable<Book>> PostBook()
        {
            //超时测试
            Thread.Sleep(15000);
            return _bookService.Get();
        }

        //[HttpPost]
        //public ActionResult<Book> Create(Book book)
        //{
        //    return new Book();
        //    //_bookService.Create(book);
        //    //return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        //}

        [HttpPut("{id:length(24)}")]
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