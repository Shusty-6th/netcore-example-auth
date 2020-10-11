using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NetCoreExampleAuth.Domain.Core.Repositories;

namespace NetCoreExampleAuth.Domain.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly RepositoryContext Context;

        public Repository(RepositoryContext context)
        {
            Context = context;
        }

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Context.Set<TEntity>().AsQueryable();
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).ToList();
        }

        ////public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        ////{
        ////    return Context.Set<TEntity>().Where(predicate);
        ////}

        ////public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        ////{
        ////    var query = Context.Set<TEntity>().Where(predicate);
        ////    return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        ////}

        ////public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        ////{
        ////    return Context.Set<TEntity>().SingleOrDefault(predicate);
        ////}

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(int id)
        {
            TEntity entityToDelete = Context.Set<TEntity>().Find(id);
            this.Remove(entityToDelete);
        }

        public void Remove(TEntity entity)
        {
            DbSet<TEntity> dbSet = this.Context.Set<TEntity>();

            if (this.Context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public void RefreshEntity(TEntity entity)
        {
            Context.Entry(entity).Reload();
        }
    }
}
