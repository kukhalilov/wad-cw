using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System;
using WebAPI.DAL;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly BookContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(BookContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Author))
            {
                var authors = _context.Authors.Include(a => a.Books).ToList();
                return (IEnumerable<T>)authors;
                
            } else
            {
                // get books with authors but without authors' books to avoid circular reference
                var books = _context.Books.Include(b => b.Author).Select(b => new Book
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

                return (IEnumerable<T>)books;
            }
        }

        public T GetById(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null && entity.GetType() == typeof(Book))
            {
                _context.Entry((Book)(object)entity).Reference(e => e.Author).Load();
            }
            if (entity != null && entity.GetType() == typeof(Author)) 
            {
                _context.Entry((Author)(object)entity).Collection(e => e.Books).Load();
            }
            
            return entity;
        }

        public void Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Add(entity);
            SaveChanges();
        }

        public void Update(int id, T entity)
        {
            var existingEntity = _context.Set<T>().Find(id);
            if (existingEntity != null)
            {
                foreach (var prop in _context.Entry(existingEntity).Properties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        continue;
                    }

                    var propName = prop.Metadata.Name;
                    var newValue = _context.Entry(entity).Property(propName).CurrentValue;
                    _context.Entry(existingEntity).Property(propName).CurrentValue = newValue;
                }

                SaveChanges();
            }
        }

        public void Delete(int id)
        {
            T entityToDelete = _dbSet.Find(id);
            if (entityToDelete == null)
                throw new ArgumentNullException(nameof(id));

            _dbSet.Remove(entityToDelete);
            SaveChanges();
        }

        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
