using Microsoft.Extensions.Configuration;
using Pets.Model;
using Pets.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pets.Sql
{
    internal class BookRepository : IBookRepository
    {
        public Book Create(Book book)
        {
            throw new NotImplementedException();
        }

        public List<Book> Get()
        {
            throw new NotImplementedException();
        }

        public Book Get(string id)
        {
            throw new NotImplementedException();
        }

        public void Remove(string id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Book bookIn)
        {
            throw new NotImplementedException();
        }

        public void Update(string id, Book bookIn)
        {
            throw new NotImplementedException();
        }
    }
}
