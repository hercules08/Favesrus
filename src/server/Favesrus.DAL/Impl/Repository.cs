using Favesrus.Common.Abstract;
using Favesrus.Common.Impl;
using Favesrus.DAL.Abstract;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Favesrus.DAL.Impl
{
    public class Repository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private readonly FavesrusDbContext context;

        public Repository(IUnitOfWork uow)
        {
            context = uow.Context as FavesrusDbContext;
        }

        public IQueryable<T> All
        {
            get { return context.Set<T>(); }
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>();

            foreach (var include in includeProperties)
            {
                query = query.Include(include);
            }

            return query;
        }

        public async Task<ICollection<T>> AllIncludingAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            return await AllIncluding(includeProperties).ToListAsync();
        }

        public T Get(int id)
        {
            return context.Set<T>().Find(id);
        }

        public Task<T> GetAsync(int id)
        {
            return context.Set<T>().FindAsync(id);
        }

        public T FindBy(Expression<Func<T, bool>> match)
        {
            return context.Set<T>().SingleOrDefault(match);
        }

        public async Task<T> FindByAsync(Expression<Func<T, bool>> match)
        {
            return await context.Set<T>().SingleOrDefaultAsync(match);
        }

        public IQueryable<T> FindAllWhere(Expression<Func<T, bool>> match)
        {
            return context.Set<T>().Where(match);
        }

        public async Task<ICollection<T>> FindAllWhereAsync(Expression<Func<T, bool>> match)
        {
            return await context.Set<T>().Where(match).ToListAsync();
        }

        public T Add(T entity)
        {
            return context.Set<T>().Add(entity);
        }

        public T Update(T entity)
        {
            context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public void Delete(T entity)
        {
            var item = context.Set<T>().Find(entity.Id);
            context.Set<T>().Remove(item);
        }

        public void Delete(int id)
        {
            var item = context.Set<T>().Find(id);
            context.Set<T>().Remove(item);
        }

        public int Count
        {
            get { return context.Set<T>().Count(); }
        }

        public async Task<int> CountAsync()
        {
            return await context.Set<T>().CountAsync();
        }

        public PaginatedList<T> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector)
        {
            throw new NotImplementedException();
        }

        public PaginatedList<T> PaginateBy<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedList<T>> PaginateAsync<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedList<T>> PaginateByAsync<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (context != null)
                context.Dispose();
        }
    }
}
