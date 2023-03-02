using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System;

namespace WebAPI.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        void Add(T entity);

        void Delete(int id);

        void Update(int id, T entityToUpdate);
    }
}
