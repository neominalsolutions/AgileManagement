using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Core
{
    /// <summary>
    /// TContext Hangi DbContext nesnesine bağlanacağımızı biliyoruz.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public abstract class EFBaseRepository<TEntity,TContext> : IRepository<TEntity>
        where TEntity : Entity
        where TContext: DbContext

    {
        protected TContext _dbContext;
        protected DbSet<TEntity> _dbSet;

        public EFBaseRepository(TContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }


        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual TEntity Find(string Id)
        {
            return _dbSet.Find(Id);
        }

        public virtual IQueryable<TEntity> GetQuery()
        {
            return _dbSet.AsQueryable();
        }

        public virtual IQueryable GetSqlRawQuery(string query)
        {


            return _dbSet.AsQueryable();


            //return _dbContext.Set<TEntity>().FromSqlRaw()
        }

        public virtual void Remove(string Id)
        {
            var entity = Find(Id);
            _dbSet.Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
