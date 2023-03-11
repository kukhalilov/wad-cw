using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Models;
using DataAccess.DAL;

namespace DataAccess.Repositories
{
    public class BookRepository : GenericRepository<Book>
    {
        public BookRepository(BookContext context) : base(context)
        {
        }

        public override IEnumerable<Book> GetAll()
        {
            // get books with authors but without authors' books to avoid circular reference
            var books = Context.Books.Include(b => b.Author).Select(b => new Book
            {
                BookId = b.BookId,
                Title = b.Title,
                PublishDate = b.PublishDate,
                Description = b.Description,
                PageCount = b.PageCount,
                AuthorId = b.AuthorId,
                Author = new Author
                {
                    AuthorId = b.Author.AuthorId,
                    Name = b.Author.Name,
                    Surname = b.Author.Surname
                },
                IsAvailable = b.IsAvailable
            }).ToList();

            return books;
        }

        public override Book GetById(int id)
        {
            var book = Context.Books.Find(id);
            if (book != null) 
            {
                Context.Entry(book).Reference(e => e.Author).Load();
            }
            return book;
        }
    }
}
