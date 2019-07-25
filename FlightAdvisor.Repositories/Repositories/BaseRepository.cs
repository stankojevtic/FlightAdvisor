using FlightAdvisor.API;
using FlightAdvisor.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightAdvisor.Repositories.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DataContext _dbContext;

        public BaseRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T Get(Func<T, bool> predicate)
        {
            return GetWhere(predicate).FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return GetWhere(null);
        }

        public IEnumerable<T> GetWhere(Func<T, bool> predicate)
        {
            IEnumerable<T> result = _dbContext.Set<T>().AsEnumerable();
            return (predicate == null) ? result : result.Where<T>(predicate);
        }

        public void Add(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            Save();
        }

        public void Delete(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            Save();
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            if (_dbContext != null)
                _dbContext.Dispose();
        }
    }
}
