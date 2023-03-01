using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAuthors();
        Author GetAuthorById(int id);
        void AddAuthor(Author Author);
        void UpdateAuthor(int id);
        void DeleteAuthor(int id);
    }
}
