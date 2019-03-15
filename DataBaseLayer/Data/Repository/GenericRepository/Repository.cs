using DataBaseAccessLayer.Data.DatabaseContext;
using DataBaseAccessLayer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataBaseAccessLayer.Data.Repository.GenericRepository
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity: class, IEntity
    {
        private readonly BlogContext context;
        private DbSet<TEntity> dbSet;

        public Repository(BlogContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public IList<TEntity> FilterBy(Expression<Func<TEntity, bool>> filter)
        {
            return this.dbSet.Where(filter).ToList();
        }

        public IQueryable<TEntity> Query()
        {
            return this.dbSet;
        }

        public IQueryable<TEntity> Get()
        {
            return Query();
        }

        public TEntity Get(long id)
        {
            return this.dbSet.Find(id);
        }

        public void Add(TEntity e)
        {
            try
            {
                this.context.Add(e);
                this.context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }            
        }

        public void Update(TEntity e)
        {
            try
            {
                this.context.Update(e);
                this.context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public void Delete(TEntity e)
        {
            if (this.dbSet.Contains(e))
            {
                try
                {
                    this.context.Remove(e);
                    this.context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            else throw new Exception("No item found");
        }

    }
}
