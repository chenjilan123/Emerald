using Pets.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pets.Repository
{
    public interface IBookRepository
    {
        List<Book> Get();
        Book Get(string id);
        Book Create(Book book);
        void Update(string id, Book bookIn);
        void Remove(string id);
        void Remove(Book bookIn);
    }
}
