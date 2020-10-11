using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NetCoreExampleAuth.Domain.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes);
        //IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        //IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        //TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(int id);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void RefreshEntity(TEntity entity);
    }
}
