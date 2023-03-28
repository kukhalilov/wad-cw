using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Models;
using DataAccess.DAL;
using System;

namespace DataAccess.Repositories
{
    public class BookRepository : GenericRepository<Book>
    {
        private readonly BookContext _context;

        public BookRepository(BookContext context) : base(context)
        {
            _context = context;
        }

        public override IEnumerable<Book> GetAll(string searchTerm, string sortBy,
                bool sortAsc, int page, int pageSize
            )
        {
            var query = _context.Books.Include(b => b.Author).AsQueryable();

            // filter by search term if provided
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(b => b.Title.Contains(searchTerm) || b.Description.Contains(searchTerm)
                || b.Author.Name.Contains(searchTerm) || b.Author.Surname.Contains(searchTerm));
            }

            // sort by property and direction if provided
            switch (sortBy)
            {
                case "title":
                    query = sortAsc ? query.OrderBy(b => b.Title) : query.OrderByDescending(b => b.Title);
                    break;
                case "publishDate":
                    query = sortAsc ? query.OrderBy(b => b.PublishDate) : query.OrderByDescending(b => b.PublishDate);
                    break;
                case "pageCount":
                    query = sortAsc ? query.OrderBy(b => b.PageCount) : query.OrderByDescending(b => b.PageCount);
                    break;
                case "authorName":
                    query = sortAsc ? query.OrderBy(b => b.Author.Name) : query.OrderByDescending(b => b.Author.Name);
                    break;
                case "authorSurname":
                    query = sortAsc 
                        ? query.OrderBy(b => b.Author.Surname) 
                        : query.OrderByDescending(b => b.Author.Surname);
                    break;
                default:
                    query = query.OrderBy(b => b.Title);
                    break;
            }

            // apply pagination
            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            // get books with authors but without authors' books to avoid circular reference
            var books = query.Select(b => new Book
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
            var book = _context.Books.Include(b => b.Author).SingleOrDefault(b => b.BookId == id);
            return book;
        }
    }
}
