using Favesrus.Domain.DomainObject;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Favesrus.DAL.Core
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> All { get; }
        IQueryable<T> FindAllWhere(Expression<Func<T, bool>> predicate);

        IPagedList<T> GetPage<TKey>(
            Page page,
            Expression<Func<T, TKey>> keySelector);

        IPagedList<T> GetPageWhereIncluding<TKey>(
            Page page,
            Expression<Func<T, TKey>> keySelector,
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties);

        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        
        void Add(T entity);
        void Update(T entity);

        void Delete(T entity);
        void DeleteWhere(Expression<Func<T, bool>> where);

        T FindById(long id);
        T FindByGuid(string id);
        T FindWhere(Expression<Func<T, bool>> where);
    }

    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        private FavesrusDbContext _dataContext;
        private readonly IDbSet<T> _dbSet;
        public RepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            _dbSet = DataContext.Set<T>();
        }

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        protected FavesrusDbContext DataContext
        {
            get { return _dataContext ?? (_dataContext = DatabaseFactory.Get()); }
        }


        public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DataContext.Set<T>();
            foreach (var includeProperty in includeProperties)
            {

                query = query.Include(includeProperty);
            }

            return query;
        }

        public IQueryable<T> All
        {
            get { return DataContext.Set<T>(); }
        }

        public IQueryable<T> FindAllWhere(Expression<Func<T, bool>> predicate)
        {
            return DataContext.Set<T>().Where(predicate);
        }

        public IPagedList<T> GetPage<TKey>(Page page, Expression<Func<T, TKey>> keySelector)
        {
            return GetPageWhereIncluding<TKey>(page, keySelector, null, null);
        }

        public IPagedList<T> GetPageWhereIncluding<TKey>(Page page, Expression<Func<T, TKey>> keySelector, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query =
                AllIncluding(includeProperties).OrderBy(keySelector);

            query = (predicate == null)
                ? query
                : query.Where(predicate);

            var results = query.GetPage(page).ToList();
            var total = query.Count(predicate);
            return new StaticPagedList<T>(results, page.PageNumber, page.PageSize, total);
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await DataContext.SaveChangesAsync();

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            DataContext.Entry(entity).State = EntityState.Modified;
            await DataContext.SaveChangesAsync();

            return entity;
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            DataContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteWhere(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = _dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                _dbSet.Remove(obj);
        }

        public T FindById(long id)
        {
            return _dbSet.Find(id);
        }

        public T FindByGuid(string id)
        {
            return _dbSet.Find(id);
        }

        public T FindWhere(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).FirstOrDefault();
        }

    }
}
