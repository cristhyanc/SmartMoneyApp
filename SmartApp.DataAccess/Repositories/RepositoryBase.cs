using Microsoft.EntityFrameworkCore;
using Microsoft.FSharp.Control;
using SmartApp.Common.Interfaces.Core;
using SmartApp.Core.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.DataAccess.Repositories
{
    public abstract class RepositoryBase<T> : IDisposable, IRepositoryBase<T> where T : class, new()
    {


        SmartAppContext _context;

        public string UserId { get ; set ; }

        public RepositoryBase(SmartAppContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        public abstract IEnumerable<T> GetAll(int skip = 0, int take = 20);
        public abstract Task<Tuple<int, IEnumerable<T>>> GetAllAsync(int skip = 0, int take = 20);
        public abstract T Get(Int64 id);
        public abstract Task<T> GetAsync(Int64 id);
        public virtual async Task Insert(T entity)
        {
            Type examType = typeof(T);
            PropertyInfo createdOn = examType.GetProperty("CreatedOn");
            createdOn.SetValue(entity, DateTime.Now);

            PropertyInfo userId = examType.GetProperty("UserId");
            createdOn.SetValue(entity, this.UserId);

            await  this._context.Set<T>().AddAsync(entity);
        }

        public virtual async Task Insert(IEnumerable<T> entities)
        {
           await this._context.Set<T>().AddRangeAsync(entities);
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

        public virtual async Task SaveChangesAsync()
        {
            await this._context.SaveChangesAsync();
        }      

        public void Dispose()
        {
            _context.Dispose();
        }
        
    }
}