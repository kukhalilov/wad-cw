using System.Collections.Generic;
using System.Linq;
using WebAPI.DAL;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookContext _dbContext;
        public AuthorRepository(BookContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteAuthor(int authorId)
        {
            var Author = _dbContext.Authors.Find(authorId);
            _dbContext.Authors.Remove(Author);
            Save();
        }
        public Author GetAuthorById(int authorId)
        {
            var Author = _dbContext.Authors.Find(authorId);
            return Author;
        }
        public IEnumerable<Author> GetAuthors()
        {
            return _dbContext.Authors.ToList();
        }
        public void AddAuthor(Author author)
        {
            _dbContext.Add(author);
            Save();
        }
        public void UpdateAuthor(int authorId)
        {
            var author = _dbContext.Authors.Find(authorId);
            _dbContext.Entry(author).State =
            Microsoft.EntityFrameworkCore.EntityState.Modified;
            Save();
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
