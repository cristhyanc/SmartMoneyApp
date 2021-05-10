using Microsoft.EntityFrameworkCore;
using Microsoft.FSharp.Control;
using SmartApp.Core.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.DataAccess.Repositories
{
    public abstract class RepositoryBase<T> : IDisposable, IRepositoryBase<T> where T : class, new()
    {


        SmartAppContext _context;

        public RepositoryBase(SmartAppContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        public abstract IEnumerable<T> GetAll();
        public abstract Task<IEnumerable<T>> GetAllAsync();
        public abstract T Get(Int64 id);
        public abstract Task<T> GetAsync(Int64 id);



        public virtual void Insert(T entity)
        {
            this._context.Set<T>().Add(entity);
        }

        public virtual void Insert(IEnumerable<T> entities)
        {
            this._context.Set<T>().AddRange(entities);
        }

        public virtual void Update(T entity)
        {
            this._context.Set<T>().Update(entity);
        }

        public virtual void Update(IEnumerable<T> entities)
        {
            this._context.Set<T>().UpdateRange(entities);
        }

        public virtual void Delete(T entity)
        {
            this._context.Set<T>().Remove(entity);
        }
        public virtual void Delete(IEnumerable<T> entities)
        {
            this._context.Set<T>().RemoveRange(entities);
        }


        public void Dispose()
        {
            _context.Dispose();
        }

      
    }
}