using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.Common.Interfaces.Core
{
  public interface IRepositoryBase<T>
    {
        public T Get(long id);
        public Task<T> GetAsync(long id);
        public IEnumerable<T> GetAll(int skip, int take);
        public Task<Tuple<int, IEnumerable<T>>> GetAllAsync(int skip, int take);
        public Task Insert(T item);
        public void Update(T item);
        public Task SaveChangesAsync();
        public void Delete(T entity);
        public void Delete(IEnumerable<T> entities);
    }
}