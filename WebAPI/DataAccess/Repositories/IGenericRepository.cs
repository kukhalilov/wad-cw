using System.Collections.Generic;

namespace DataAccess.Repositories
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
