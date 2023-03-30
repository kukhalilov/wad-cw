using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        (IEnumerable<T>, int count) GetAll(string searchTerm, string sortBy,
                bool sortAsc = true, int page = 1, int pageSize = 10
            );

        T GetById(int id);

        void Add(T entity);

        void Delete(int id);

        void Update(int id, T entityToUpdate);
    }
}
