using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using DataAccess.Models;

namespace DataAccess.DAL
{
    public class BookContext: DbContext
    {
        public BookContext(DbContextOptions<BookContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);
        }

        public void AddSampleData()
        {
            if (!Authors.Any() && !Books.Any())
            {
                var author1 = new Author { Name = "J.K.", Surname = "Rowling" };
                var author2 = new Author { Name = "George R.R.", Surname = "Martin" };
                var author3 = new Author { Name = "Stephen", Surname = "King" };
                var author4 = new Author { Name = "Dan", Surname = "Brown" };

                Authors.AddRange(author1, author2, author3, author4);
                SaveChanges();

                var book1 = new Book
                {
                    Title = "Harry Potter and the Philosopher's Stone",
                    PublishDate = new DateTime(1997, 6, 26),
                    Description = "The first book in the Harry Potter series",
                    PageCount = 223,
                    AuthorId = author1.AuthorId,
                    Author = author1,
                    IsAvailable = true
                };
                var book2 = new Book
                {
                    Title = "A Game of Thrones",
                    PublishDate = new DateTime(1996, 8, 1),
                    Description = "The first book in A Song of Ice and Fire series",
                    PageCount = 694,
                    AuthorId = author2.AuthorId,
                    Author = author2,
                    IsAvailable = false
                };
                var book3 = new Book
                {
                    Title = "Harry Potter and the Chamber of Secrets",
                    PublishDate = new DateTime(1998, 7, 2),
                    Description = "The second book in the Harry Potter series",
                    PageCount = 251,
                    AuthorId = author1.AuthorId,
                    Author = author1,
                    IsAvailable = true
                };
                var book4 = new Book
                {
                    Title = "The Shining",
                    PublishDate = new DateTime(1977, 1, 28),
                    Description = "A horror novel by Stephen King",
                    PageCount = 447,
                    AuthorId = author3.AuthorId,
                    Author = author3,
                    IsAvailable = true
                };
                var book5 = new Book
                {
                    Title = "The Da Vinci Code",
                    PublishDate = new DateTime(2003, 3, 18),
                    Description = "A mystery thriller novel by Dan Brown",
                    PageCount = 689,
                    AuthorId = author4.AuthorId,
                    Author = author4,
                    IsAvailable = false
                };
                var book6 = new Book
                {
                    Title = "Angels & Demons",
                    PublishDate = new DateTime(2000, 5, 16),
                    Description = "A mystery thriller novel by Dan Brown",
                    PageCount = 736,
                    AuthorId = author4.AuthorId,
                    Author = author4,
                    IsAvailable = true
                };

                Books.AddRange(book1, book2, book3, book4, book5, book6);
                SaveChanges();
            }
        }
    }
}
