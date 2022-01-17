using BooksService.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace BooksService.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(IBooksDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        public List<Book> Get() =>
            _books.Find(b => true).ToList();

        // METTERE TRY-CATCH IN TUTTI I METODI DEL SERVICE??
        public Book Get(string id)
        {
            try
            {
                return _books.Find<Book>(b => b.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                // log the exception
                return null;
            }
        }


        public Book Create(Book books)
        {
            _books.InsertOne(books);
            return books;
        }

        public void Update(string id, Book books) =>
            _books.ReplaceOne(b => b.Id == id, books);

        public void Remove(string id) =>
            _books.DeleteOne(b => b.Id == id);
    }
}
