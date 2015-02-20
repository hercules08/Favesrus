using Favesrus.Common.Abstract;
using Favesrus.Common.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.DAL.Abstract
{
    public interface IRepository<T>:IDisposable where T : class, IEntity, new ()
    {
        IQueryable<T> All { get; }
        Task<ICollection<T>> GetAllAsync();
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<ICollection<T>> AllIncludingAsync(params Expression<Func<T, object>>[] includeProperties);
        T Get(int id);
        Task<T> GetAsync(int id);
        T FindBy(Expression<Func<T, bool>> match);
        Task<T> FindByAsync(Expression<Func<T, bool>> match);
        IQueryable<T> FindAllWhere(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FindAllWhereAsync(Expression<Func<T, bool>> match);
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        int Count { get; }
        Task<int> CountAsync();
        PaginatedList<T> Paginate<TKey>(
            int pageIndex, int pageSize,
            Expression<Func<T, TKey>> keySelector);
        PaginatedList<T> PaginateBy<TKey>(
            int pageIndex, int pageSize,
            Expression<Func<T, TKey>> keySelector,
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties);
        Task<PaginatedList<T>> PaginateAsync<TKey>(
            int pageIndex, int pageSize,
            Expression<Func<T, TKey>> keySelector);
        Task<PaginatedList<T>> PaginateByAsync<TKey>(
            int pageIndex, int pageSize,
            Expression<Func<T, TKey>> keySelector,
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties);
    }
}
