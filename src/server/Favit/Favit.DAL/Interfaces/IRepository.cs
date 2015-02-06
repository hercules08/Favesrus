using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Favit.DAL.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Gets the uo W.
        /// </summary>
        /// <value>The uo W.</value>
        IUnitOfWork UoW { get; }

        /// <summary>
        /// Adds the entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        T AddEntity<T>(T entity) where T : class;

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        T UpdateEntity<T>(T entity) where T : class;

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        void DeleteEntity<T>(T entity) where T : class;

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetList<T>() where T : class;

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        IQueryable<T> GetList<T>(Expression<Func<T, bool>> query) where T : class;

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <param name="primaryKey">The primary key.</param>
        /// <returns></returns>
        T GetEntity<T>(object primaryKey) where T : class;
    }
}
