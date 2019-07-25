using System;
using System.Collections.Generic;

namespace FlightAdvisor.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    {
        T Get(Func<T, bool> predicate);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetWhere(Func<T, bool> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
