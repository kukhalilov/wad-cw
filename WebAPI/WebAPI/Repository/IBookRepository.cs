using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetBooks();
        Book GetBookById(int id);
        void AddBook(Book book);
        void UpdateBook(int id);
        void DeleteBook(int id);
    }
}
