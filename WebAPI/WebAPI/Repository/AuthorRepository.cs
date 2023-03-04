using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebAPI.DAL;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class AuthorRepository : GenericRepository<Author>
    {
        public AuthorRepository(BookContext context) : base(context)
        {
        }

        public override IEnumerable<Author> GetAll()
        {
            var authors = Context.Authors.Include(a => a.Books).ToList();
            return authors;
        }

        public override Author GetById(int id)
        {
            var author = Context.Authors.Find(id);
            if (author != null)
            {
                Context.Entry(author).Collection(e => e.Books).Load();
            }
            return author;
        }
    }
}
