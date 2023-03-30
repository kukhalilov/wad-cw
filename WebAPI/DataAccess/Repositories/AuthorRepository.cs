using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Models;
using DataAccess.DAL;
using System.Globalization;
using System;

namespace DataAccess.Repositories
{
    public class AuthorRepository : GenericRepository<Author>
    {
        public AuthorRepository(BookContext context) : base(context)
        {
        }

        public override (IEnumerable<Author>, int count) GetAll(string searchTerm, string sortBy,
                bool sortAsc, int page, int pageSize
            )
        {
            var query = Context.Authors.Include(a => a.Books).AsQueryable();
            var count = query.Count();

            // filter by search term if provided
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(a => a.Name.Contains(searchTerm) || a.Surname.Contains(searchTerm));
                count = query.Count();
            }

            // sort by property and direction if provided
            switch (sortBy)
            {
                case "name":
                    query = sortAsc ? query.OrderBy(a => a.Name) : query.OrderByDescending(a => a.Name);
                    break;
                case "surname":
                    query = sortAsc ? query.OrderBy(a => a.Surname) : query.OrderByDescending(a => a.Surname);
                    break;
                default:
                    query = query.OrderBy(a => a.Name);
                    break;
            }

            // apply pagination
            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var authors = query.ToList();

            return (authors, count);
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
