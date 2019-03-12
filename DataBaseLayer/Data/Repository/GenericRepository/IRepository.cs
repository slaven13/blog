using DataBaseAccessLayer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataBaseAccessLayer.Data.Repository.GenericRepository
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        IQueryable<TEntity> Query();

        IList<TEntity> FilterBy(Expression<Func<TEntity, bool>> func);

        IQueryable<TEntity> Get();

        TEntity Get(long id);

        void Add(TEntity e);

        void Update(TEntity e);

        void Delete(TEntity e);
    }
}
