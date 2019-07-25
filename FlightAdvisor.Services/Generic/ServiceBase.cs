//using FlightAdvisor.Data.Generic;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace FlightAdvisor.Core.Generic
//{
//    //public class ServiceBase<T> : IServiceBase<T> where T : class
//    //{
//    //    private readonly IBaseRepository<T> _repository;

//    //    public ServiceBase(IGenericRepository<T> repository)
//    //    {
//    //        _repository = repository;
//    //    }

//    //    public virtual IQueryable<T> GetAll()
//    //    {
//    //        return _repository.GetAll();
//    //    }

//    //    public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
//    //    {
//    //        return _repository.FindBy(predicate);
//    //    }

//    //    public async Task<T> FindAsync(params object[] keys)
//    //    {
//    //        return await _repository.FindAsync(keys);
//    //    }

//    //    public virtual async Task AddAsync(T entity)
//    //    {
//    //        await _repository.AddAsync(entity);
//    //        await _repository.SaveChangesAsync();
//    //    }

//    //    public virtual async Task DeleteAsync(T entity)
//    //    {
//    //        _repository.Delete(entity);
//    //        await _repository.SaveChangesAsync();
//    //    }

//    //    public virtual async Task DeleteAsync(params object[] keys)
//    //    {
//    //        var entity = await _repository.FindAsync(keys);
//    //        _repository.Delete(entity);
//    //        await _repository.SaveChangesAsync();
//    //    }

//    //    public virtual async Task UpdateAsync(T entity)
//    //    {
//    //        await _repository.UpdateAsync(entity);
//    //        await _repository.SaveChangesAsync();
//    //    }
//    //}
//}
