using NBuyFirstGroup.Core.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Core
{
    /// <summary>
    /// Abstract class olan Entityden kalıtım alan birşeye ancak Repository uygulanabilir.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity:Entity
    {
        void Add(TEntity entity);
        void Remove(string Id);
        void Update(TEntity entity);

        TEntity Find(string Id);

        /// <summary>
        /// Linq query
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetQuery();
        /// <summary>
        /// Sql query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IQueryable GetSqlRawQuery(string query);
    }
}
