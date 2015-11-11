using System.Collections.Generic;

namespace CT.Repository.Infrastructure
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        void InsertAndSubmit(T entity);
        void UpdateAndSubmit(T entity);
        void DeleteAndSubmit(T entity);
        T GetById(int id);
        void SoftDeleteAndSubmit(T entity);
    }
}
