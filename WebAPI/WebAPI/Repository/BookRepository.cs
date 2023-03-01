using System.Collections.Generic;
using System.Linq;
using System.Net;
using WebAPI.DAL;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class BookRepository: IBookRepository
    {
        private readonly BookContext _dbContext;
        public BookRepository(BookContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteBook(int bookId)
        {
            var book = _dbContext.Books.Find(bookId);
            _dbContext.Books.Remove(book);
            Save();
        }
        public Book GetBookById(int bookId)
        {
            var book = _dbContext.Books.Find(bookId);
            return book;
        }
        public IEnumerable<Book> GetBooks()
        {
            return _dbContext.Books.ToList();
        }
        public void AddBook(Book book)
        {
            _dbContext.Add(book);
            Save();
        }
        public void UpdateBook(int bookId)
        {
            var book = _dbContext.Books.Find(bookId);
            _dbContext.Entry(book).State =
            Microsoft.EntityFrameworkCore.EntityState.Modified;
            Save();
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
