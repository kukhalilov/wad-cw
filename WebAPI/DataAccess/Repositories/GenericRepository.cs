using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using DataAccess.DAL;
using DataAccess.Logging;
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>, IObservable where T : class
    {
        private readonly DbSet<T> _dbSet;
        protected readonly BookContext Context;
        private readonly List<IObserver> _observers;

        public GenericRepository(BookContext context)
        {
            Context = context;
            _dbSet = context.Set<T>();
            _observers = new List<IObserver>();
        }

        public virtual IEnumerable<T> GetAll(string searchTerm, string sortBy,
                bool sortAsc = true, int page = 1, int pageSize = 10
            )
        {
            return _dbSet.ToList();
        }

        public virtual T GetById(int id)
        {
            var entity = _dbSet.Find(id);
            return entity;
        }

        public virtual void Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Add(entity);
            SaveChanges();

            Notify($"{DateTime.Now}: new {entity.GetType()} added");
        }

        public virtual void Update(int id, T entity)
        {
            var existingEntity = Context.Set<T>().Find(id);
            if (existingEntity != null)
            {
                foreach (var prop in Context.Entry(existingEntity).Properties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        continue;
                    }

                    var propName = prop.Metadata.Name;
                    var newValue = Context.Entry(entity).Property(propName).CurrentValue;
                    Context.Entry(existingEntity).Property(propName).CurrentValue = newValue;
                }

                SaveChanges();
            }
        }

        public virtual void Delete(int id)
        {
            T entityToDelete = _dbSet.Find(id);
            if (entityToDelete == null)
                throw new ArgumentNullException(nameof(id));

            _dbSet.Remove(entityToDelete);
            SaveChanges();
        }

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(string message)
        {
            foreach (var observer in _observers)
            {
                observer.Update(message);
            }
        }

        public void SaveChanges()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
