using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using DataAccess.Models;
using System.Collections.Generic;

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

        private Author CreateNewAuthor(string name, string surname)
        {
            return new Author { Name = name, Surname = surname };
        }

        private Book CreateNewBook(string title, DateTime publishDate, string description, int pageCount, Author author, bool isAvailable)
        {
            return new Book
            {
                Title = title,
                PublishDate = publishDate,
                Description = description,
                PageCount = pageCount,
                Author = author,
                IsAvailable = isAvailable
            };
        }

        public void AddSampleData()
        {
            if (!Authors.Any() && !Books.Any())
            {
                // create 8 authors
                var authors = new List<Author>
                {
                    CreateNewAuthor("J.K.", "Rowling"),
                    CreateNewAuthor("George R.R.", "Martin"),
                    CreateNewAuthor("Stephen", "King"),
                    CreateNewAuthor("Dan", "Brown"),
                    CreateNewAuthor("J.R.R.", "Tolkien"),
                    CreateNewAuthor("Agatha", "Christie"),
                    CreateNewAuthor("Ernest", "Hemingway"),
                    CreateNewAuthor("Gabriel", "García Márquez")
                };

                Authors.AddRange(authors);
                SaveChanges();

                var books = new List<Book>
                {
                    CreateNewBook("Harry Potter and the Philosopher's Stone", new DateTime(1997, 6, 26), "The first book in the Harry Potter series", 223, authors[0], true),
                    CreateNewBook("A Game of Thrones", new DateTime(1996, 8, 1), "The first book in A Song of Ice and Fire series", 694, authors[1], false),
                    CreateNewBook("Harry Potter and the Chamber of Secrets", new DateTime(1998, 7, 2), "The second book in the Harry Potter series", 251, authors[0], true),
                    CreateNewBook("The Shining", new DateTime(1977, 1, 28), "A horror novel by Stephen King", 447, authors[2], true),
                    CreateNewBook("The Da Vinci Code", new DateTime(2003, 3, 18), "A mystery thriller novel by Dan Brown", 689, authors[3], false),
                    CreateNewBook("Angels & Demons", new DateTime(2000, 5, 16), "A mystery thriller novel by Dan Brown", 736, authors[3], true),
                    CreateNewBook("The Lord of the Rings: The Fellowship of the Ring", new DateTime(1954, 7, 29), "The first novel in The Lord of the Rings trilogy", 423, authors[4], true),
                    CreateNewBook("The Lord of the Rings: The Two Towers", new DateTime(1954, 11, 11), "The second novel in The Lord of the Rings trilogy", 352, authors[4], false),
                    CreateNewBook("The Lord of the Rings: The Return of the King", new DateTime(1955, 10, 20), "The third novel in The Lord of the Rings trilogy", 416, authors[4], true),
                    CreateNewBook("Murder on the Orient Express", new DateTime(1934, 1, 1), "A detective novel by Agatha Christie", 256, authors[5], true),
                    CreateNewBook("And Then There Were None", new DateTime(1939, 11, 6), "A mystery novel by Agatha Christie", 264, authors[5], false),
                    CreateNewBook("The Old Man and the Sea", new DateTime(1952, 9, 1), "A novel by Ernest Hemingway", 127, authors[6], true),
                    CreateNewBook("For Whom the Bell Tolls", new DateTime(1940, 10, 21), "A novel by Ernest Hemingway", 480, authors[6], true),
                    CreateNewBook("One Hundred Years of Solitude", new DateTime(1967, 5, 30), "A novel by Gabriel García Márquez", 417, authors[7], true),
                    CreateNewBook("Love in the Time of Cholera", new DateTime(1985, 3, 27), "A novel by Gabriel García Márquez", 348, authors[7], false),
                    CreateNewBook("The Hobbit", new DateTime(1937, 9, 21), "A novel by J.R.R. Tolkien", 310, authors[4], true),
                    CreateNewBook("The Silmarillion", new DateTime(1977, 9, 15), "A collection of J.R.R. Tolkien's works on Middle-earth", 365, authors[4], true),
                    CreateNewBook("The Casual Vacancy", new DateTime(2012, 9, 27), "A novel by J.K. Rowling", 503, authors[0], false),
                    CreateNewBook("A Clash of Kings", new DateTime(1998, 11, 16), "The second book in A Song of Ice and Fire series", 761, authors[1], true),
                    CreateNewBook("The Winds of Winter", new DateTime(2023, 7, 1), "The upcoming sixth book in A Song of Ice and Fire series", 800, authors[1], false),
                    CreateNewBook("The Stand", new DateTime(1978, 10, 3), "A post-apocalyptic horror novel by Stephen King", 1152, authors[2], true),
                    CreateNewBook("The Dark Tower: The Gunslinger", new DateTime(1982, 6, 10), "The first book in The Dark Tower series", 231, authors[2], false),
                    CreateNewBook("Digital Fortress", new DateTime(1998, 4, 22), "A techno-thriller novel by Dan Brown", 384, authors[3], true),
                    CreateNewBook("Inferno", new DateTime(2013, 5, 14), "A mystery thriller novel by Dan Brown", 480, authors[3], false),
                    CreateNewBook("Murder at the Vicarage", new DateTime(1930, 10, 6), "A detective novel by Agatha Christie", 288, authors[5], true),
                    CreateNewBook("Death on the Nile", new DateTime(1937, 11, 1), "A detective novel by Agatha Christie", 288, authors[5], false),
                    CreateNewBook("Love and Other Demons", new DateTime(1994, 3, 2), "A novel by Gabriel García Márquez", 160, authors[7], true)
                };

                Books.AddRange(books);
                SaveChanges();
            }
        }
    }
}
