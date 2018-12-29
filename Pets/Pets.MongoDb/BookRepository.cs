using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Pets.Model;
using Pets.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pets.MongoDb
{
    internal class BookRepository : IBookRepository
    {
        private readonly IMongoCollection<Book> _books;
        public BookRepository(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("BookstoreDb"));
            var database = client.GetDatabase("BookstoreDb");
            _books = database.GetCollection<Book>("Books");
        }
        //public BookService(string connectionStr) //这样子是有依赖的。
        //{
        //    var client = new MongoClient(connectionStr);
        //    var database = client.GetDatabase("BookstoreDb");
        //    _books = database.GetCollection<Book>("Books");
        //}

        public List<Book> Get()
        {
            return _books.Find(book => true).ToList();
        }

        public Book Get(string id)
        {
            //var docId = new ObjectId(id);
            return _books.Find(book => book.Id == id).FirstOrDefault();
        }

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Book bookIn)
        {
            //var docId = new ObjectId(id);
            _books.ReplaceOne(book => book.Id == id, bookIn);
        }

        public void Remove(string id)
        {
            _books.DeleteOne(book => book.Id == id);
        }

        public void Remove(Book bookIn)
        {
            _books.DeleteOne(book => book.Id == bookIn.Id);
        }
    }
}
